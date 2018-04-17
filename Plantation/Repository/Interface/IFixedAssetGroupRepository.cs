using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IFixedAssetGroupRepository
    {
        List<FixedAssetGroup> GetAll();
        FixedAssetGroup Find(int? SID);
        FixedAssetGroup Add(FixedAssetGroup FAG, string userid);
        FixedAssetGroup Update(FixedAssetGroup FAG, string userid);
        void Remove(int SID);
    }
}