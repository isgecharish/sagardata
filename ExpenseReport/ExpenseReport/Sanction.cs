using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace ExpenseReport
{
    public class Sanction
    {
        public  string ConTest = ConfigurationManager.AppSettings["ConnectionTest"];
        public  string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public string Division { get; set; }
        public string ProjectCode { get; set; }
       

       static string Con;
        
        public DataTable GetDivision(string env)
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
            return SqlHelper.ExecuteDataset(Sanction.Con, CommandType.Text, "Select Distinct Division FROM LN_TravelSiteSanction").Tables[0];
        }
        public DataTable GetProjectCode(string env)
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
            return SqlHelper.ExecuteDataset(Sanction.Con, CommandType.Text, "Select Distinct ProjectCode FROM LN_TravelSiteSanction where Division='"+ Division + "'").Tables[0];
        }
        public DataTable GeTraveltSanctionBalance(string env)
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
            return SqlHelper.ExecuteDataset(Sanction.Con, CommandType.Text, @"SELECT DISTINCT ProjectCode, Division, ProjectDescription,
            SUM(ISNULL(L3Level,0)) as 'L3Level', SUM(ISNULL(L4Level,0))as 'L4Level'
            FROM( Select ProjectCode,Division,ProjectDescription ,
            CASE WHEN ElementCode='99250000' THEN Round(BalanceAmount,2) end as 'L3Level',
            CASE WHEN ElementCode='99250100' THEN Round(BalanceAmount,2)  end as 'L4Level'
            FROM LN_TravelSiteSanction
            where Division='" + Division + @"' and ProjectCode IN(" + ProjectCode + @") and ElementCode!='99020000' and ElementCode!='99022500'
            and ProjectCode in(select distinct Project from LN_IRData where InvoiceDate >= DATEADD(DAY, -90, GETDATE()) )) T group by ProjectCode,Division,ProjectDescription order by ProjectCode").Tables[0];
        }
        public DataTable GeSitetSanctionBalance(string env)
        {
            if (env == "P")
            {
                Con = ConLive;
            }
            else if(env=="T")
            {
                Con = ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Sanction.Con, CommandType.Text, @"SELECT DISTINCT ProjectCode, Division, ProjectDescription,
            SUM(ISNULL(L3Level,0)) as 'L3Level', SUM(ISNULL(L4Level,0))as 'L4Level'
            FROM(Select DISTINCT(ProjectCode),Division,ProjectDescription ,
            CASE WHEN ElementCode='99020000' THEN Round(BalanceAmount,2) end as 'L3Level',
            CASE WHEN ElementCode='99022500' THEN Round(BalanceAmount,2)  end as 'L4Level'
            FROM LN_TravelSiteSanction
            where Division='" + Division + @"' and ProjectCode IN(" + ProjectCode + @") and ElementCode!='99250000' and ElementCode!='99250100'
            and ProjectCode in(select distinct Project from LN_IRData where InvoiceDate >= DATEADD(DAY, -90, GETDATE()) )) T group by ProjectCode,Division,ProjectDescription order by ProjectCode").Tables[0];
        }
    }
}