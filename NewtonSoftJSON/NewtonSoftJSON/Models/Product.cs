using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewtonSoftJSON.Models
{
    public class Product
    {
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Price  { get; set; }
        public List<string> Sizes { get; set; }
    }
    
}