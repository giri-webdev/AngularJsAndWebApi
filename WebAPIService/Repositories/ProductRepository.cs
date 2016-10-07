using ServiceLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceLayer.ViewModels;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace ServiceLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public List<CategoryViewModel> ListProducts()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.
                ConnectionStrings["DataServerConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT p.ProductID, p.CategoryID, p.ProductName,p.UnitPrice,c.CategoryName,c.[Description] FROM Products p
                                                 INNER JOIN Categories c on p.CategoryID = c.CategoryID",conn);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                List<CategoryViewModel> products = dt.AsEnumerable().
                    GroupBy(x => x.Field<string>("CategoryName")).Select(x => new CategoryViewModel
                {
                    Name = x.Key,
                    Products = (from product in x
                                select new ProductViewModel
                                {
                                    ID = product.Field<int>("ProductID"),
                                    Name = product.Field<string>("ProductName"),
                                    Price = product.Field<decimal>("UnitPrice"),
                                    CategoryID = product.Field<int>("CategoryID"),

                                }).ToList(),
                    CategoryID=x.Select(y=>y.Field<int>("CategoryID")).FirstOrDefault(),
                    Description=x.Select(y=>y.Field<string>("Description")).FirstOrDefault()
                }).ToList();

                return products;
            }
        }

        public bool AddToCart(CartViewModel viewModel)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.
               ConnectionStrings["DataServerConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Cart VALUES(@ProductID,@ISActive)",conn);
                cmd.Parameters.AddWithValue("@ProductID", viewModel.ProductID);
                cmd.Parameters.AddWithValue("@ISActive", viewModel.IsActive);
               int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}