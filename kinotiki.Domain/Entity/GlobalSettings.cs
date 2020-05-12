using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace kinotiki.Domain.Entity
{
    [Table("Global_Settings")]
    public class GlobalSettings
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string smtpIP { get; set; }
        public int smtpPort { get; set; }
        public string smtpMail { get; set; }
        public string smtpPassword { get; set; }
    }
}
