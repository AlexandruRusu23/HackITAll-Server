using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackITAll.Models
{
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public double? Latitude { get; set; }
        [Required]
        public double? Longitude { get; set; }

        public string Role { get; set; }

        public bool ValidatedUser { get; set; }



        [HiddenInput, NotMapped]
        public string ReturnUrl { get; set; }


        public ICollection<Product> Products { get; set; }

    }
}