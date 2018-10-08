using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IBlockUsageRepository
    {
        List<BlockUsage> GetAll();
        BlockUsage Find(int? SID);
        BlockUsage Add(BlockUsage BU);
        BlockUsage Update(BlockUsage BU);
        void Remove(int SID);
        int GetTotalBlockUsage(int BlockMaster);
    }
}