using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }

    }
}