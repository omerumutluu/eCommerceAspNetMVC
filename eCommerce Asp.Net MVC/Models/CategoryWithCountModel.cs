using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce_Asp.Net_MVC.Models
{
    public class CategoryWithCountModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
    }
}