using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace kinotiki.Web.Models.Account
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Enter login")]
        public string login { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string password { get; set; }
    }
}