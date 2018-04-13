using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
   public class SqlConn
    {
        public static string TABillCon = ConfigurationManager.AppSettings["TABillConnection"];
        public static string ProjectCon = ConfigurationManager.AppSettings["ProjectConnection"];
    }
}
