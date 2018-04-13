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
    public partial class GenerateOpeningBalance : System.Web.UI.Page
    {
        Sanction objSanction;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
            }

        }

        private void BindProjects()
        {
            try
            {
                objSanction = new Sanction();
                DataTable dt = objSanction.BindProductsServer();
                ddlProjectFrom.DataValueField = "t_cprj";
                ddlProjectFrom.DataTextField = "t_cprj";
                ddlProjectFrom.DataSource = dt;
                ddlProjectFrom.DataBind();

                ddlProjectTo.DataValueField = "t_cprj";
                ddlProjectTo.DataTextField = "t_cprj";
                ddlProjectTo.DataSource = dt;
                ddlProjectTo.DataBind();
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            objSanction = new Sanction();
            DataTable dtOpeningBalance = objSanction.GetAllOpeningBalance();

            DataTable dt = objSanction.GetOpeningBalance(ddlProjectFrom.SelectedValue, ddlProjectTo.SelectedValue);

            for (int j = 0; j < dtOpeningBalance.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ProjectCode"].ToString() == dtOpeningBalance.Rows[j]["ProjectCode"].ToString())
                        dt.Rows.RemoveAt(i);
                    else
                        i++;
                }
            }
            int res = objSanction.InsertBalance(dt);
        }
    }
}