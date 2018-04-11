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
    public partial class ViewBalances : System.Web.UI.Page
    {
        Sanction objSanction;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetOpeningBalance();
            }
        }

        protected void GetOpeningBalance()
        {
            objSanction = new Sanction();
            DataTable dt= objSanction.GetAllOpeningBalance();
            gvOpeningBalance.DataSource = dt;
            gvOpeningBalance.DataBind();
        }
    }
}