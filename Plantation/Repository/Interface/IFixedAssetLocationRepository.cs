using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IFixedAssetLocationRepository
    {
        List<FixedAssetLocation> GetAll();
        FixedAssetLocation Find(int? SID);
        FixedAssetLocation Add(FixedAssetLocation FAL, string userid);
        FixedAssetLocation Update(FixedAssetLocation FAL, string userid);
        void Remove(int SID);
    }
}