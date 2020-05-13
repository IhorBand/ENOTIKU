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

        public byte[] imageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string imageMimeType { get; set; }

        public string BirthdayDate { get; set; }

        public int BirthdayYear
        {
            get
            {
                int m = 0;
                var s = BirthdayDate.Split('-');
                Int32.TryParse(s.Length > 0 ? s[0] : DateTime.Now.Year.ToString(), out m);
                return m;
            }
        }

        public int BirthdayMonth
        {
            get
            {
                int m = 0;
                var s = BirthdayDate.Split('-');
                Int32.TryParse(s.Length > 1 ? s[1] : DateTime.Now.Month.ToString(), out m);
                return m;
            }
        }

        public int BirthdayDay
        {
            get
            {
                int m = 0;
                var s = BirthdayDate.Split('-');
                Int32.TryParse(s.Length > 2 ? s[2] : DateTime.Now.Day.ToString(), out m);
                return m;
            }
        }
    }
}