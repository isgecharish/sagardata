using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WCFAPI
{
    public class AttachmentCls
    {
        string ConAthCon = ConfigurationManager.AppSettings["Connection"];
        public string DocumentId { get; set; }
        public string IndexValue { get; set; }
        public string AttachmentHandle { get; set; }
        public string PurposeCode { get; set; }
        public string FileName { get; set; }
        public string LibraryCode { get; set; }
        public string AttachedBy { get; set; }
        public DataTable GetAttachment(string AthHandleS, string IndexS)
        {
            return SqlHelper.ExecuteDataset(ConAthCon, CommandType.Text, @"select * from ttcisg132200 
                            where t_hndl='" + AthHandleS+ "' and t_indx ='" + IndexS+"'").Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable Insertdata()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@PurposeCode", PurposeCode));
            sqlParamater.Add(new SqlParameter("@FileName", FileName));
            sqlParamater.Add(new SqlParameter("@LibraryCode", LibraryCode));
            sqlParamater.Add(new SqlParameter("@AttachedBy", AttachedBy));
            sqlParamater.Add(new SqlParameter("@DocumnetId", DocumentId));
            return SqlHelper.ExecuteDataset(ConAthCon, CommandType.StoredProcedure, "[sp_CopyAttachment]", sqlParamater.ToArray()).Tables[0];
        }

    }
}
