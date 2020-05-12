using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinotiki.BLL.Entity
{
    public class GlobalSettings
    {
        public int id { get; set; }
        public string smtpIP { get; set; }
        public int smtpPort { get; set; }
        public string smtpMail { get; set; }
        public string smtpPassword { get; set; }
    }
}
