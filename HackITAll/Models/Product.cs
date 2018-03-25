using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackITAll.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public double? Quantity { get; set; }

        public string Description { get; set; }
        public byte[] Picture { get; set; }


        public int UserId { get; set; }
        public Users User { get; set; }

    }
}