using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public string Description { get; set; }
    }
}