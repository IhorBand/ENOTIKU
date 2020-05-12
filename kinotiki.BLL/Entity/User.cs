using kinotiki.BLL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinotiki.BLL.Entity
{
    public class User
    {
        public int id { get; set; }

        public string login { get; set; }
        public string password { get; set; }

        public string email { get; set; }
        public int age { get; set; }

        public GenderType sex { get; set; }

        public RoleType role { get; set; }

        public byte[] imageData { get; set; }
        public string imageMimeType { get; set; }
    }
}
