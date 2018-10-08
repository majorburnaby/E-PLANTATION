using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IBlockOrganizationRepository
    {
        List<BlockOrganization> GetAll();
        BlockOrganization Find(int? SID);
        BlockOrganization Add(BlockOrganization BLO, string userid);
        BlockOrganization Update(BlockOrganization BLO, string userid);
        void Remove(int SID);
    }
}