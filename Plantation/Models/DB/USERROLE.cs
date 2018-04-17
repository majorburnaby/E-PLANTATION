using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class UserRole
    {
        public int SID { get; set; }
        public int IDUSER { get; set; }
        public int IDROLE { get; set; }
        public bool ISACTIVE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }

        public virtual UserProfile USERPROFILE { get; set; }
    }
}