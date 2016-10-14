using System;

namespace ServiceLayer.ViewModels
{
    public class CartViewModel
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public bool IsActive { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }
    }
}