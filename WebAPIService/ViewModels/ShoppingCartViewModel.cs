﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ViewModels
{
    public class CartViewModel
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public bool IsActive { get; set; }
    }
}