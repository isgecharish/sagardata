using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManHoursGroup
{
    public partial class AllHoursBarChartReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static GlobalClass[] BindHours(string Year)
        {
            string listItem = string.Empty;
            DataTable dt = new DataTable();
            List<GlobalClass> listMothlyHours = new List<GlobalClass>();
            GlobalClass objGlobalClass = new GlobalClass();

            dt = objGlobalClass.GetMonthlyPlannedForChart();

            foreach (DataRow dr in dt.Rows)
            {
                objGlobalClass = new GlobalClass();
                objGlobalClass.Month = dr["MN"].ToString();
                objGlobalClass.MonthName = dr["Month"].ToString();
                objGlobalClass.PlannedHours = dr["PlannedHours"].ToString();
                objGlobalClass.ActualHours = dr["ActualHours"].ToString();
                objGlobalClass.AvailableHours = dr["AvailableHours"].ToString();
                listMothlyHours.Add(objGlobalClass);
            }
            return listMothlyHours.ToArray();
        }
    }
}