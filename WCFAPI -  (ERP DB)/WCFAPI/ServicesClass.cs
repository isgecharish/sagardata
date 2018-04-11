using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WCFAPI
{
    public class ServicesClass
    {
        string con = ConfigurationManager.AppSettings["Connection"];
     
        public string projectCode { get; set; }
        public string projectName { get; set; }
    
        public DataTable GetProject()
        {
            return SqlHelper.ExecuteDataset(con, CommandType.Text, "select t_cprj,t_dsca from ttcmcs052200 where t_cprj NOT LIKE '[0-9]%'").Tables[0];
        }
    }
}