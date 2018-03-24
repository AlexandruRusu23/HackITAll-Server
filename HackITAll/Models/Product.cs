using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackITAll.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }


        public int UserId { get; set; }
        public Users User { get; set; }

    }
}