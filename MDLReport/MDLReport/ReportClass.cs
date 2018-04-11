using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace MDLReport
{
    public class ReportClass
    {
        public static string ConTest = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];
        public string Project { get; set; }
        public string InputDate { get; set; }
        public string ProjectFrom { get; set; }
        public string ProjectTo { get; set; }
        public string Year { get; set; }
        public string MonthFrom { get; set; }
        public string MonthTo { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string GetEnv(string env)
        {
            if (env == "P")
            {
                Con = ConLive;
            }
            else if (env == "T")
            {
                Con = ConTest;
            }
            else
            {
                Con = "";
            }
            return Con;
        }
        public DataSet GetMDLReport(string Connection)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@InputDate", InputDate));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "[sp_MDL_Report]", sqlParamater.ToArray());
        }
        public DataSet GetLN_OverallEngineeringSummary(string Connection, string cmpId)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@ProjectFrom",ProjectFrom));
            sqlParamater.Add(new SqlParameter("@ProjectTo", ProjectTo));
            sqlParamater.Add(new SqlParameter("@ReportDate", InputDate));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "LNSP_PMDL_OverallEngineeringSummary_"+cmpId+"", sqlParamater.ToArray());
        }
        public DataTable GetProjectFromPMDL(string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, "SELECT Distinct Project from LN_PMDL order by Project").Tables[0];
        }
        public DataTable GetProjectFromPMDLPMAL(string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, @"SELECT Distinct Project from LN_PMDL
                                    UNION SELECT Distinct Project from LN_PMAL
                                    where Project != '' and Project != 'CHECKER'
                                    order by Project").Tables[0];
        }
        public DataSet GetManHourConsumed_Disciplinewise(string Connection)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@InputDate", InputDate));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "[sp_ManHourConsumed_Disciplinewise]", sqlParamater.ToArray());
        }
        public DataSet GetPlannedPercProgress(string Connection)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@Year", Year));
            sqlParamater.Add(new SqlParameter("@MonthFrom", MonthFrom));
            sqlParamater.Add(new SqlParameter("@MonthTo", MonthTo));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "[sp_GetPlannedPercProgress]", sqlParamater.ToArray());
        }
        public DataTable GetYear(string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, "select distinct Year(BaselineScheduleFinishDate) as year from LN_PMDL order by year desc").Tables[0];
        }
        public DataTable GetMonth(string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, @"select distinct(MONTH(BaselineScheduleFinishDate)) as Month,datename(MONTH ,BaselineScheduleFinishDate) as Monthname from LN_PMDL  ORDER BY Month").Tables[0];
        }
    
        public DataTable GetOTP_ProjectEng(string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, @"select Project,Element,DocumentID,DocumentDescription,ResponsibleDiscipline,BaselineScheduleFinishDate,
                            LatestRevisedScheduleFinishDate,ActualReleaseDate,
                            CASE WHEN ActualReleaseDate>BaselineScheduleFinishDate THEN 'Delay'
                            WHEN ActualReleaseDate<=BaselineScheduleFinishDate and ActualReleaseDate>'01-Jan-1990'  THEN 'Ontime'
                            ELSE 'Delay'
                            END As BaseLineStatus,
                            CASE WHEN ActualReleaseDate>LatestRevisedScheduleFinishDate and ActualReleaseDate>'01-Jan-1990' THEN 'Delay'
                            WHEN ActualReleaseDate<=LatestRevisedScheduleFinishDate THEN 'Ontime'
                            ELSE 'Delay'
                            END As RevisedScheduleStatus,
                            ReasonforScheduleVariance,CorrectivePreventiveAction
                            FROM LN_PMDL
                            where Project between  '" + ProjectFrom+ @"' and '" + ProjectTo + @"'
                            And  BaselineScheduleFinishDate between '"+DateFrom+"' and '"+DateTo+"'").Tables[0];
        }

        public DataSet GetonTimePerfomanceReport(string Connection)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@ProjectFrom", ProjectFrom));
            sqlParamater.Add(new SqlParameter("@ProjectTo", ProjectTo));
            sqlParamater.Add(new SqlParameter("@DateFrom", DateFrom));
            sqlParamater.Add(new SqlParameter("@DateTo", DateTo));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "sp_OntimePerformanceWiseReport",sqlParamater.ToArray());
        }

        public DataSet GetElementStatus_ProjectWise(string Connection)
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@ProjectFrom", ProjectFrom));
            sqlParamater.Add(new SqlParameter("@ProjectTo", ProjectTo));
            return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "[sp_ElementStatus_ProjectWise]", sqlParamater.ToArray());
        }
    }
}