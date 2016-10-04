using System.Collections.Generic;

namespace ServiceLayer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Products { get; set; }
    }
}