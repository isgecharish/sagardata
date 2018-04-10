using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectProgressUpdate
{
    public partial class ProjectLogin : System.Web.UI.Page
    {
        ProjectClass objProjectCls;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            // { 

            // if (Response.Cookies["UserName"].Value != "")
            //     txtUserName.Text = Response.Cookies["UserName"].Value;
            // if (Response.Cookies["Password"].Value != "")
            //     txtUserName.Text = Response.Cookies["Password"].Value;
            // }
            
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
           
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            if (!(txtUserName.Text).Length.Equals(0) && password != string.Empty)
            {
                //if (chkRememberMe.Checked)
                //{
                //    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                //    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                //}
                //else
                //{
                //    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                //}
                //Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                //Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                ValidateUser(username, password);
            }
            else
            {
                lblError.Visible = true;
            }
            
        }
        protected void ValidateUser(string Id, string password)
            {

            objProjectCls = new ProjectClass();
            DataTable dt = new DataTable();
            dt = objProjectCls.GetUserRecord(Id, password);
            Session["Username"] = Id;
            if (dt.Rows.Count > 0)
            {
                Session["projId"] = dt.Rows[0]["t_cprj"];
                Session["day"] = dt.Rows[0]["t_day"];
                Session["projName"] = dt.Rows[0]["t_dsca"];
                Response.Redirect("ProjectProgressReport.aspx");
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "alert", "alert('The Login Id and/or password you have entered is invalid');", true);


            }
        }
    }
}
