using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BusinessLogic
{
    public class SqlConn
    {
        public static string ConTest = ConfigurationManager.AppSettings["PackageConnectionTest"];
        public static string ConLive = ConfigurationManager.AppSettings["PackageConnectionLive"];
    }
}
