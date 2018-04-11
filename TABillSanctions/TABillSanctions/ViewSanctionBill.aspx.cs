using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using System.Data;
namespace TABillSanctions
{
    public partial class ViewSanctionBill : System.Web.UI.Page
    {

        Sanction objSanction = new Sanction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBills();
            }
        }
        protected void BindBills()
        {
            try
            {
                objSanction = new Sanction();
                DataTable dt = objSanction.GetSanctionBillsHistory();
                gvBill.DataSource = dt;
                gvBill.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

    }
}