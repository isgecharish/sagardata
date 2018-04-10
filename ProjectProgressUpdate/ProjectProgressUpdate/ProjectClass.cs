using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectProgressUpdate
{
    public class ProjectClass
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public DataTable GetProjectProgressByID(string username,string ProjectId,string Activity)
        {
            string sGetProjectProgress = @"select t_cprj,t_cact,t_sdst,t_sdfn,t_acsd,t_acfn, t_dsca,t_remk from ttpisg910200 WHERE t_cprj = '" + ProjectId + "' and t_cact='" + Activity + "'";
            if (username == "isgec")

            { return SqlHelper.ExecuteDataset(Con, CommandType.Text, sGetProjectProgress).Tables[0]; }
            else
            { 
            return SqlHelper.ExecuteDataset(ConLive, CommandType.Text, sGetProjectProgress).Tables[0];
            }
        }
        public int UpdatRecords(string username,string ProjectId, string Activity,string Actual_SDate, string Actual_FDate, string Remarks)
        {
            string sUpdateRecords = @"Update ttpisg910200 set t_acsd='" + Actual_SDate + "' ,t_remk='" + Remarks + "', t_acfn='" + Actual_FDate + @"'
                            WHERE t_cprj = '" + ProjectId + "' and t_cact='" + Activity + "'";
            if (username == "isgec")
            {
                return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, sUpdateRecords);
            }
            else
            { 
            return SqlHelper.ExecuteNonQuery(ConLive, CommandType.Text, sUpdateRecords);
            }
        }
        public DataTable GetUserRecord(string LoginId, string Password)
        {
            string sUserDetails = @"select a.t_cprj, a.t_day, b.t_dsca from ttpisg225200 a inner join ttcmcs052200 b on a.t_cprj=b.t_cprj where t_logn= '" + LoginId + "' and t_pwd='" + Password + "'";
            if (LoginId == "isgec")
            {
                return SqlHelper.ExecuteDataset(Con, CommandType.Text, sUserDetails).Tables[0];
            }
            else
            { 
            return SqlHelper.ExecuteDataset(ConLive, CommandType.Text, sUserDetails).Tables[0];
            }
        }
    }
}