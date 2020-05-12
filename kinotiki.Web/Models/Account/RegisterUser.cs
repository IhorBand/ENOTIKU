using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using kinotiki.Domain.Entity;

namespace kinotiki.Web.Models.Account
{
    public class RegisterUser
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Enter login")]
        public string login { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Enter email")]
        public string email { get; set; }
        public int sex { get; set; }
        public int age { get; set; }

        public byte[] imageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string imageMimeType { get; set; }
    }
}