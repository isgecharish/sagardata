using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTest
{
    public partial class TestLogin : System.Web.UI.Page
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
            //txtUserName.Text = "isgec";
            //txtPassword.Text = "isgec";
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
                lblError.Visible = false;
            }
            
        }
        protected void ValidateUser(string Id, string password)
            {

            objProjectCls = new ProjectClass();
            DataTable dt = new DataTable();
            int userrecord = objProjectCls.ValidateUser(Id, password);
            if (userrecord > 0)
            {
                lblError.Visible = false;
               dt = objProjectCls.GetUserRecord(Id);
                Session.Add("RecordTable", dt);
                Session["Username"] = Id;
                if (dt.Rows.Count > 0)
                {
                    Session["projId"] = dt.Rows[0]["t_cprj"];
                    Session["day"] = dt.Rows[0]["t_day"];
                    Session["projName"] = dt.Rows[0]["t_dsca"];
                    Session["Dept"] = dt.Rows[0]["t_dept"];
                   // Session["DrpProj"] = dt.Rows[0]["drpdwn"];
                    Response.Redirect("ProjectProgressReport.aspx");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page),
                        "alert", "alert('User is not associated with any Project');", true);


                }
            }
            else
            {
                lblError.Visible = true;
                
                //ScriptManager.RegisterStartupScript(this, typeof(Page),
                //        "alert", "alert('The Username and/or password you have entered is invalid');", true);
            }
        }
    }
}
