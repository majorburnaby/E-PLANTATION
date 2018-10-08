using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IFieldMasterRepository
    {
        List<FieldMaster> GetAll();
        List<FieldMaster> GetAllByCompanySite(int? CompanySite);
        FieldMaster Find(int? SID);
        FieldMaster Add(FieldMaster FM, string userid);
        FieldMaster Update(FieldMaster FM, string userid);
        void Remove(int SID);
    }
}