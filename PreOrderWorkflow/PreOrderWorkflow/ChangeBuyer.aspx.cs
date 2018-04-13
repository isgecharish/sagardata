using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PreOrderWorkflow
{
    public partial class ChangeBuyer : System.Web.UI.Page
    {
        WorkFlow objWorkFlow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["WFID"] != null && Request.QueryString["u"] != null)
                {
                    GetData();
                }
            }
        }

        private void GetData()
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
            DataTable dt = objWorkFlow.GetWFById();
            DataTable dtGetNotes = objWorkFlow.GetHistory();
            if (dt.Rows.Count > 0)
            {
                txtProject.Text = dt.Rows[0]["Project"].ToString();
                txtElement.Text = dt.Rows[0]["Element"].ToString();
                txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                txtStatus.Text = dt.Rows[0]["WF_Status"].ToString();
                txtCreater.Text = dt.Rows[0]["EmployeeName"].ToString();
                txtDAte.Text = Convert.ToDateTime(dt.Rows[0]["DateTime"].ToString()).ToString("dd-MM-yyyy");
               // hdfUser.Value = dt.Rows[0]["UserId"].ToString();
            }
            if (dtGetNotes.Rows.Count > 0)
            {
                txtNotes.Text = dtGetNotes.Rows[0]["Notes"].ToString();
            }
        }
        
        [WebMethod]
        public static string[] GetUSer(string prefixText, int count)
        {
            WorkFlow objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetUser(prefixText);
            List<string> lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string UserId = dr["EmployeeName"].ToString() + "-" + dr["CardNo"].ToString();
                lst.Add(UserId);
            }
            return lst.ToArray();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBuyer.Text != "")
            {
                string[] Buyer = txtBuyer.Text.Split('-');
                objWorkFlow = new WorkFlow();
                objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
                objWorkFlow.Buyer = Buyer[1];
                int res = objWorkFlow.UpdateBuyer();
                if (res > 0)
                {
                    InsertPreHistory(Convert.ToInt32(Request.QueryString["WFID"]), "Change Buyer");
                    Response.Redirect("ReleaseTechnicalSpecification.aspx?u=" + Request.QueryString["u"]);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Select Buyer');", true);
            }
        }
        private void InsertPreHistory(int Id, string status)
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Id;
            DataTable dt = objWorkFlow.GetWFById();
            objWorkFlow.Parent_WFID = Convert.ToInt32(dt.Rows[0]["Parent_WFID"]);
            objWorkFlow.Project = dt.Rows[0]["Project"].ToString();
            objWorkFlow.Element = dt.Rows[0]["Element"].ToString();
            objWorkFlow.SpecificationNo = dt.Rows[0]["SpecificationNo"].ToString();
            objWorkFlow.Buyer = dt.Rows[0]["Buyer"].ToString();
            objWorkFlow.UserId = Request.QueryString["u"];
            objWorkFlow.WF_Status = status;
            objWorkFlow.Supplier = dt.Rows[0]["Supplier"].ToString();
            objWorkFlow.SupplierName = dt.Rows[0]["SupplierName"].ToString();
            objWorkFlow.InserPreOrderHistory();
        }
    }
}