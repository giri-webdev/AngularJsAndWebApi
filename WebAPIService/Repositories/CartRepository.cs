using ServiceLayer.Repositories.Interfaces;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ServiceLayer.Repositories
{
    public class CartRepository : ICartRepository
    {

        public bool AddToCart(CartViewModel viewModel)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.
               ConnectionStrings["DataServerConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Cart VALUES(@ProductID,@ISActive,@Date,@UserId)", conn);
                cmd.Parameters.AddWithValue("@ProductID", viewModel.ProductID);
                cmd.Parameters.AddWithValue("@ISActive", viewModel.IsActive);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserId", viewModel.UserId);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    return true;
                else
                    return false;
            }
        }

        public List<CartViewModel> ListProducts(string userID)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.
                ConnectionStrings["DataServerConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT  c.in_cart_id, p.ProductID, p.ProductName, p.UnitPrice,c.dt_date FROM Products p
INNER JOIN Cart c ON p.ProductID = c.in_product_id
WHERE c.bi_active ='true' and c.vc_user_id =@userId", conn);
                cmd.Parameters.AddWithValue("@userId", userID);

                List<CartViewModel> items = new List<CartViewModel>();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CartViewModel item = new CartViewModel();
                    item.ID = reader.GetInt32(0);
                    item.ProductID = reader.GetInt32(1);
                    item.ProductName = reader.GetString(2);
                    item.UnitPrice = reader.GetDecimal(3);
                    item.Date = reader.GetDateTime(4);
                    items.Add(item);
                }

                return items;

            }
        }

        public bool DeleteItem(int id)
        {
            return true;
        }

    }
}