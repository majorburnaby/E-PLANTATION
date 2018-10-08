using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class UserProfile
    {
        public UserProfile()
        {
            this.USERCOMPANies = new HashSet<USERCOMPANY>();
            this.USERCOMPANYSITEs = new HashSet<UserCompanySite>();
            this.USERROLEs = new HashSet<UserRole>();
        }

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
        public string DESCRIPTION { get; set; }
        public bool ISACTIVE { get; set; }
        public string EMAIL { get; set; }
        public System.DateTime ACTIVEFROMDATE { get; set; }
        public System.DateTime ACTIVEENDDATE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public Nullable<System.DateTime> LASTLOGINDATE { get; set; }
        public string LASTLOGINIPADDRESS { get; set; }
        public Nullable<System.DateTime> LASTCHANGEPASSWORD { get; set; }

        public virtual ICollection<USERCOMPANY> USERCOMPANies { get; set; }
        public virtual ICollection<UserCompanySite> USERCOMPANYSITEs { get; set; }
        public virtual ICollection<UserRole> USERROLEs { get; set; }
    }
}