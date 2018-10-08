using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IBlockMasterRepository
    {
        List<BlockMaster> GetAll();
        List<BlockMaster> GetAllByCompanySite(int CompanySite);
        BlockMaster Find(int? SID);
        BlockMaster Add(BlockMaster BLO, string userid);
        BlockMaster Update(BlockMaster BLO);
        void Remove(int SID);
        bool HasBlockUsage(int SID);
        void UpdateHectarage(int SID, int Hectarage);
    }
}