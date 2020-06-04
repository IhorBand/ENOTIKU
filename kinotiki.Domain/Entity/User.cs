using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace kinotiki.Domain.Entity
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public string login { get; set; }
        public string password { get; set; }

        public string email { get; set; }
        public int sex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Birthday { get; set; }

        public int role { get; set; }

        public byte[] imageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string imageMimeType { get; set; }        

        public bool isDarkTheme { get; set; }

        public bool isEmailVerificated { get; set; }

        public string verificationKey { get; set; }
    }
}
