using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ManHoursGroup
{
    public class GlobalClass
    {
        public static string ConTest = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];
        public string Year { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string MonthFrom { get; set; }
        public string MonthTo { get; set; }
        public string Project { get; set; }
        public string ProjectFrom { get; set; }
        public string ProjectTo { get; set; }
        public string RoleType { get; set; }
        public string GroupId { get; set; }
        public string PlannedHours { get; set; }
        public string ActualHours { get; set; }
        public string AvailableHours { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string DisciplineFrom { get; set; }
        public string DisciplineTo { get; set; }
        public DataTable GetAvailableActualHours()
        {
            string qrystr = string.Empty;
            if (Year != "")
            {
                qrystr = qrystr + " and Year in(" + Year + ")";
            }
            if (Month != "")
            {
                qrystr = qrystr + " and MONTH(Date1) in(" + Month + ")";
            }
            if (RoleType != "")
            {
                qrystr = qrystr + " and RoleType in(" + RoleType + ")";
            }
            if (GroupId != "")
            {
                qrystr = qrystr + " and GroupID in(" + GroupId + ")";
            }
            //string q = @"select *, Right(Year(Date1), 4)+'/' + cast(month(Date1) as varchar) as MonthYear
            //        from LN_PMDLAvailableActualHours
            //        WHERE 1=1 " + qrystr + "";

            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select *, Right(Year(Date1), 4)+'/' + cast(month(Date1) as varchar) as MonthYear
                    from LN_PMDLAvailableActualHours
                    WHERE 1=1 " + qrystr + "").Tables[0];
        }
        public DataTable GetPlannedHours()
        {
            string qrystr = string.Empty;
            if (Year != "")
            {
                qrystr = qrystr + " and Year in(" + Year + ")";
            }
            if (Month != "")
            {
                qrystr = qrystr + " and MONTH(Date1) in(" + Month + ")";
            }
            if (RoleType != "")
            {
                qrystr = qrystr + " and RoleType in(" + RoleType + ")";
            }
            if (GroupId != "")
            {
                qrystr = qrystr + " and GroupID in(" + GroupId + ")";
            }
            if (Project != "")
            {
                qrystr = qrystr + " and Project in(" + Project + ")";
            }
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select *, Right(Year(Date1),4)+ '/' + cast(month(Date1) as varchar) as MonthYear 
                     from LN_PMDLPLannedHours
                     WHERE 1=1 " + qrystr + "").Tables[0];
        }
        public DataSet GetManHoursReport()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@Year", Year));
            sqlParamater.Add(new SqlParameter("@Month", Month));
            sqlParamater.Add(new SqlParameter("@Projects", Project));
            sqlParamater.Add(new SqlParameter("@Roles", RoleType));
            sqlParamater.Add(new SqlParameter("@ManhoursGroups", GroupId));
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.StoredProcedure, "ManHoursReport", sqlParamater.ToArray());
        }
        public DataTable GetYear()
        {
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, "select distinct Year as year from LN_PMDLAvailableActualHours UNION select distinct Year as year from LN_PMDLPLannedHours").Tables[0];
        }
        public DataTable GetMonth()
        {
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select distinct(MONTH(Date1)) as Month,datename(MONTH ,Date1) as Monthname from LN_PMDLAvailableActualHours
                                UNION select distinct(MONTH(Date1)) as Month, datename(MONTH, Date1) as Monthname from LN_PMDLPLannedHours
                                ORDER BY Month").Tables[0];
        }
        public DataTable GetProject()
        {

            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, "SELECT distinct Project from LN_PMDLPLannedHours order by Project").Tables[0];
        }
        public DataTable GetRoleType()
        {
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, "SELECT distinct RoleType from LN_PMDLAvailableActualHours UNION SELECT distinct RoleType from LN_PMDLPLannedHours").Tables[0];
        }
        public DataTable GetManhoursGroup()
        {
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, "SELECT distinct GroupID from LN_PMDLAvailableActualHours UNION SELECT distinct GroupID from LN_PMDLPLannedHours").Tables[0];
        }
        public DataTable GetMonthlyWisePlannedReport()
        {
            //return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"Select * from (select  Project,ProjectDescription,MONTH(Date1) as [Month],PlannedHours
            //                 from LN_PMDLPLannedHours where Year in("+Year+@") and month(Date1) between '"+MonthFrom+@"' and '"+MonthTo+@"') as t
            //                 PIVOT
            //                (
            //                    SUM(PlannedHours) FOR[Month] in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
            //                )AS pvt
            //                ").Tables[0];
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@YearFrom", YearFrom));
            sqlParamater.Add(new SqlParameter("@YearTo", YearTo));
            sqlParamater.Add(new SqlParameter("@FromMonth", MonthFrom));
            sqlParamater.Add(new SqlParameter("@ToMonth", MonthTo));
            sqlParamater.Add(new SqlParameter("@cols1", ""));
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.StoredProcedure, "[spLNPMDLDashboard]", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable GetMonthlyPlannedForChart()
        {
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select Distinct DateName(mm,DATEADD(mm,Month,-1))AS 'Month',Month as MN, sum(PlannedHours) as PlannedHours,sum(AvailableHours) as AvailableHours
                            ,sum(ActualHours)as ActualHours from(
                            select MONTH(Date1) As Month ,SUM(PlannedHours) as PlannedHours ,'' as AvailableHours,'' AS ActualHours
                            FROM LN_PMDLPLannedHours
                            where Year=2017
                            group by MONTH(Date1)
                            UNION
                            Select MONTH(Date1) as Month ,'' as PlannedHours,
                            SUM(AvailableHours) as AvailableHours,SUM(ActualHours) AS ActualHours
                            From LN_PMDLAvailableActualHours
                            where Year=2017
                            group by MONTH(Date1)
                            )T group by T.Month order by MN").Tables[0];
        }
        public DataTable GetProjectFromPMDL(string env)
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
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, "SELECT Distinct Project from LN_PMDL order by Project").Tables[0];
        }

        public DataTable GetResponsible_Discipline(string env)
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
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"SELECT Distinct ResponsibleDiscipline from LN_PMDL 
                                            where ResponsibleDiscipline != ''
                                            order by ResponsibleDiscipline").Tables[0];
        }
        public DataTable GetPMDLDATA(string env)
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
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select Project  ,t_dsca
                          , DUID
                          , LatestRevision
                          , DocumentID
                          , DocumentDescription
                          , Element
                          , DocumentCategoryID
                          , ResponsibleDiscipline
                          , DocumentSizeID
                          , DocumentOwner
                          , PlannedDrawnBy
                          , PlannedCheckedBy
                          , PlannedEngineer
                          , BaselineScheduleStartDate
                          , BaselineScheduleFinishDate
                          , ActualReleaseDate
                          ,LatestRevisedScheduleStartDate
                          ,LatestRevisedScheduleFinishDate
                     FROM LN_PMDL
 INNER JOIN ttcmcs052200 p ON  p.t_cprj=LN_PMDL.Project
                     WHERE Project between '" + ProjectFrom + @"' and '" + ProjectTo + @"'
                     and LatestRevisedScheduleFinishDate between '" + DateFrom + @"' and '" + DateTo + @"'
                     and ResponsibleDiscipline  between '" + DisciplineFrom + @"' and '" + DisciplineTo+ @"'
                     order by Project").Tables[0];
        }

        public DataTable GetPendingPMDLDATA(string env)
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
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select Project  ,t_dsca
                          , DUID
                          , LatestRevision
                          , DocumentID
                          , DocumentDescription
                          , Element
                          , DocumentCategoryID
                          , ResponsibleDiscipline
                          , DocumentSizeID
                          , DocumentOwner
                          , PlannedDrawnBy
                          , PlannedCheckedBy
                          , PlannedEngineer
                          , BaselineScheduleStartDate
                          , BaselineScheduleFinishDate
                          , ActualReleaseDate
                          ,LatestRevisedScheduleStartDate
                          ,LatestRevisedScheduleFinishDate
                     FROM LN_PMDL
 INNER JOIN ttcmcs052200 p ON  p.t_cprj=LN_PMDL.Project
                     WHERE Project between '" + ProjectFrom + @"' and '" + ProjectTo + @"'
                     and LatestRevisedScheduleFinishDate between '1970-01-01 00:00:00' and '" + DateTo + @"'
                     and ActualReleaseDate='1970-01-01 00:00:00.000'
                     and ResponsibleDiscipline  between '" + DisciplineFrom + @"' and '" + DisciplineTo + @"'
                     order by Project").Tables[0];
        }

        public DataTable GetOnlyRelesaedPMDLDATA(string env)
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
            return SqlHelper.ExecuteDataset(GlobalClass.Con, CommandType.Text, @"select Project  ,t_dsca
                          , DUID
                          , LatestRevision
                          , DocumentID
                          , DocumentDescription
                          , Element
                          , DocumentCategoryID
                          , ResponsibleDiscipline
                          , DocumentSizeID
                          , DocumentOwner
                          , PlannedDrawnBy
                          , PlannedCheckedBy
                          , PlannedEngineer
                          , BaselineScheduleStartDate
                          , BaselineScheduleFinishDate
                          , ActualReleaseDate
                          ,LatestRevisedScheduleStartDate
                          ,LatestRevisedScheduleFinishDate
                     FROM LN_PMDL
 INNER JOIN ttcmcs052200 p ON  p.t_cprj=LN_PMDL.Project
                     WHERE Project between '" + ProjectFrom + @"' and '" + ProjectTo + @"'
                     and LatestRevisedScheduleFinishDate between '" + DateFrom + @"' and '" + DateTo + @"'
                     and ActualReleaseDate!='1970-01-01 00:00:00.000'
                     and ResponsibleDiscipline  between '" + DisciplineFrom + @"' and '" + DisciplineTo + @"'
                     order by Project").Tables[0];
        }
    }
}