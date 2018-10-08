﻿using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ILandConcessionRepository
    {
        List<LandConcession> GetAll();
        List<LandConcession> GetAllByCompanySite(int? CompanySite);
        LandConcession Find(int? SID);
        LandConcession Add(LandConcession LCC, string userid);
        LandConcession Update(LandConcession LCC, string userid);
        void Remove(int SID);
    }
}