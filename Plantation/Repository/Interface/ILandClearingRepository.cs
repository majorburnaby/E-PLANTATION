﻿using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ILandClearingRepository
    {
        List<LandClearing> GetAll();
        List<LandClearing> GetAllByCompanySite(int? CompanySite);
        LandClearing Find(int? SID);
        LandClearing Add(LandClearing LCP, string userid);
        LandClearing Update(LandClearing LCP, string userid);
        void Remove(int SID);
    }
}