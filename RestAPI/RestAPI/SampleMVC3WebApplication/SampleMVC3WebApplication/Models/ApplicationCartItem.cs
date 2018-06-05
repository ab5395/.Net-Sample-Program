using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMVC3WebApplication.Models
{
    public class ApplicationCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}