using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IFixedAssetRepository
    {
        List<FixedAsset> GetAll();
        FixedAsset Find(int? SID);
        FixedAsset Add(FixedAsset FA, string userid);
        FixedAsset Update(FixedAsset FA, string userid);
        void Remove(int SID);
    }
}