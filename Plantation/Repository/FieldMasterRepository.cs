using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class FieldMasterRepository : IFieldMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);        

        public FieldMaster Add(FieldMaster FM, string userid)
        {
            var sqlQuery = @"INSERT INTO FIELDMASTER (IDFIELDMASTER, FIELDMASTERNAME, BLOCKMASTER, CROPTYPE, PROGENY, PLANTINGMATERIAL, HECTPLANTED, PLANTINGPOINTNUM, PLANTINGDISTANCE, TOTALSTAND, PLANTINGDATE, STANDPERHECT, EMPTYHOLE, FIELDSTATUS, HARVCOMMDATE, HECTHARV, OWNERSHIP, CARRYDISTANCE, ISACTIVE, ISACTIVEDATE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                ('" + FM.IDFIELDMASTER + @"', '" + FM.FIELDMASTERNAME + @"', '" + FM.BLOCKMASTER + @"', '" + FM.CROPTYPE + @"', '" + FM.PROGENY + @"', '" + FM.PLANTINGMATERIAL + @"', '" + FM.HECTPLANTED + @"', '" + FM.PLANTINGPOINTNUM + @"', '" + FM.PLANTINGDISTANCE + @"', '" + FM.TOTALSTAND + @"', '" + FM.PLANTINGDATE + @"', '" + FM.STANDPERHECT + @"', '" + FM.EMPTYHOLE + @"', '" + FM.FIELDSTATUS + @"', '" + FM.HARVCOMMDATE + @"', '" + FM.HECTHARV + @"', '" + FM.OWNERSHIP + @"', '" + FM.CARRYDISTANCE + @"', '" + FM.ISACTIVE + @"', '" + FM.ISACTIVEDATE + @"', '" + FM.COMPANYSITE + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, FM).Single();
            FM.SID = SID;
            return FM;
        }

        public FieldMaster Find(int? SID)
        {
            return this._db.Query<FieldMaster>("SELECT * FROM FIELDMASTER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<FieldMaster> GetAll()
        {
            return this._db.Query<FieldMaster>("SELECT" +
                               " FM.SID," +
                               " FM.IDFIELDMASTER," +
                               " FM.FIELDMASTERNAME," +
                               " FM.BLOCKMASTER," +
                               " FM.CROPTYPE," +
                               " FM.PROGENY," +
                               " FM.PLANTINGMATERIAL," +
                               " FM.HECTPLANTED," +
                               " FM.PLANTINGPOINTNUM," +
                               " FM.PLANTINGDISTANCE," +
                               " FM.TOTALSTAND," +
                               " FM.PLANTINGDATE," +
                               " FM.STANDPERHECT," +
                               " FM.EMPTYHOLE," +
                               " FM.FIELDSTATUS," +
                               " FM.HARVCOMMDATE," +
                               " FM.HECTHARV," +
                               " FM.OWNERSHIP," +
                               " FM.CARRYDISTANCE," +
                               " FM.ISACTIVE," +
                               " FM.ISACTIVEDATE," +
                               " FM.COMPANYSITE," +
                               " FM.INPUTBY," +
                               " FM.INPUTDATE," +
                               " FM.UPDATEBY," +
                               " FM.UPDATEDATE," +
                               " BLOCKMASTER.BLOCKMASTERNAME," +
                               " CROP.CROPNAME," +
                               " PM.PARAMETERVALUENAME as PLANTINGMATERIALNAME," +
                               " PD.PARAMETERVALUENAME as PLANTINGDISTANCENAME," +
                               " FS.PARAMETERVALUENAME as FIELDSTATUSNAME," +
                               " OW.PARAMETERVALUENAME as OWNERSHIPNAME," +
                               " PG.PARAMETERVALUENAME as PROGENYNAME" +
                               " FROM" +
                               " FIELDMASTER AS FM" +
                               " LEFT JOIN BLOCKMASTER ON FM.BLOCKMASTER = BLOCKMASTER.SID" +
                               " LEFT JOIN CROP ON FM.CROPTYPE = CROP.SID" +
                               " LEFT JOIN PARAMETERVALUE PM ON FM.PLANTINGMATERIAL = PM.SID" +
                               " LEFT JOIN PARAMETERVALUE PD ON  FM.PLANTINGDISTANCE = PD.SID" +
                               " LEFT JOIN PARAMETERVALUE FS ON  FM.FIELDSTATUS = FS.SID" +
                               " LEFT JOIN PARAMETERVALUE OW ON  FM.OWNERSHIP = OW.SID" +
                               " LEFT JOIN PARAMETERVALUE PG ON  FM.PROGENY = PG.SID" +
                               " ORDER BY" +
                               " FM.SID ASC").ToList();
        }

        
        public List<FieldMaster> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<FieldMaster>("SELECT" +
                               " FM.SID," +
                               " FM.IDFIELDMASTER," +
                               " FM.FIELDMASTERNAME," +
                               " FM.BLOCKMASTER," +
                               " FM.CROPTYPE," +
                               " FM.PROGENY," +
                               " FM.PLANTINGMATERIAL," +
                               " FM.HECTPLANTED," +
                               " FM.PLANTINGPOINTNUM," +
                               " FM.PLANTINGDISTANCE," +
                               " FM.TOTALSTAND," +
                               " FM.PLANTINGDATE," +
                               " FM.STANDPERHECT," +
                               " FM.EMPTYHOLE," +
                               " FM.FIELDSTATUS," +
                               " FM.HARVCOMMDATE," +
                               " FM.HECTHARV," +
                               " FM.OWNERSHIP," +
                               " FM.CARRYDISTANCE," +
                               " FM.ISACTIVE," +
                               " FM.ISACTIVEDATE," +
                               " FM.COMPANYSITE," +
                               " FM.INPUTBY," +
                               " FM.INPUTDATE," +
                               " FM.UPDATEBY," +
                               " FM.UPDATEDATE," +
                               " BLOCKMASTER.BLOCKMASTERNAME," +
                               " CROP.CROPNAME," +
                               " PM.PARAMETERVALUENAME as PLANTINGMATERIALNAME," +
                               " PD.PARAMETERVALUENAME as PLANTINGDISTANCENAME," +
                               " FS.PARAMETERVALUENAME as FIELDSTATUSNAME," +
                               " OW.PARAMETERVALUENAME as OWNERSHIPNAME," +
                               " PG.PARAMETERVALUENAME as PROGENYNAME" +
                               " FROM" +
                               " FIELDMASTER AS FM" +
                               " LEFT JOIN BLOCKMASTER ON FM.BLOCKMASTER = BLOCKMASTER.SID" +
                               " LEFT JOIN CROP ON FM.CROPTYPE = CROP.SID" +
                               " LEFT JOIN PARAMETERVALUE PM ON FM.PLANTINGMATERIAL = PM.SID" +
                               " LEFT JOIN PARAMETERVALUE PD ON  FM.PLANTINGDISTANCE = PD.SID" +
                               " LEFT JOIN PARAMETERVALUE FS ON  FM.FIELDSTATUS = FS.SID" +
                               " LEFT JOIN PARAMETERVALUE OW ON  FM.OWNERSHIP = OW.SID" +
                               " LEFT JOIN PARAMETERVALUE PG ON  FM.PROGENY = PG.SID" +
                               " WHERE FM.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " FM.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From FieldMaster Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public FieldMaster Update(FieldMaster FM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE FIELDMASTER SET FIELDMASTERNAME = '{0}', BLOCKMASTER = '{1}', CROPTYPE = '{2}',PROGENY = '{3}',  PLANTINGMATERIAL = '{4}', HECTPLANTED = '{5}', PLANTINGPOINTNUM = '{6}', PLANTINGDISTANCE = '{7}', TOTALSTAND = '{8}', PLANTINGDATE = '{9}', STANDPERHECT = '{10}', EMPTYHOLE = {11}, FIELDSTATUS = {12}, HARVCOMMDATE = '{13}', HECTHARV = '{14}', [OWNERSHIP] = '{15}', CARRYDISTANCE = '{16}', ISACTIVE = '{17}',  ISACTIVEDATE = '{18}',  COMPANYSITE = '{19}', UPDATEBY = {20}, UPDATEDATE = '{21}' WHERE SID = {22}",
                FM.FIELDMASTERNAME, FM.BLOCKMASTER, FM.CROPTYPE, FM.PROGENY, FM.PLANTINGMATERIAL, FM.HECTPLANTED, FM.PLANTINGPOINTNUM, FM.PLANTINGDISTANCE, FM.TOTALSTAND, FM.PLANTINGDATE, FM.STANDPERHECT, FM.EMPTYHOLE,  FM.FIELDSTATUS, FM.HARVCOMMDATE, FM.HECTHARV, FM.OWNERSHIP, FM.CARRYDISTANCE, FM.ISACTIVE, FM.ISACTIVEDATE, FM.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, FM.SID);
            this._db.Execute(sqlQuery, FM);
            return FM;
        }

    }
}