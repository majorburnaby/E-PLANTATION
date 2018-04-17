using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Login
    {
        public int SID { get; set; }
        public string IDUSER { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string USERNAME { get; set; }
        public int POSITION { get; set; }
        public int DEPARTMENT { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string USERPASSWORD { get; set; }
    }
}