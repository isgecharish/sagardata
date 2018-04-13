using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IsgecServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select IsgecServices.svc or IsgecServices.svc.cs at the Solution Explorer and start debugging.
    public class IsgecServices : IIsgecServices
    {
        public List<ServicesClass> ProjectCode()
        {
            ServicesClass objData = new ServicesClass();
            DataTable dt = objData.GetProject();
          
            List<ServicesClass> lst = new List<ServicesClass>();
            foreach (DataRow dr in dt.Rows)
            {
                objData = new ServicesClass();
                objData.projectCode = dr["t_cprj"].ToString();
                objData.projectName = dr["t_dsca"].ToString();
                lst.Add(objData);
            }

            return lst;
        }
    }
}
