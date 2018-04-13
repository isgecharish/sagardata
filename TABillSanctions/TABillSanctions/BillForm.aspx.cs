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
    public partial class BillForm : System.Web.UI.Page
    {
        Sanction objSanction;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
                if (Request.QueryString["RequestNumber"] != null)
                {
                    BindBillDetails(Convert.ToInt32(Request.QueryString["RequestNumber"]));
                    btnSave.Text = "Update";
                }
                else
                {
                    //objSanction = new Sanction();
                    //DataTable dt = objSanction.getMaxRequestNo();
                    //txtRequestNo.Text = dt.Rows[0][0].ToString();
                }
            }
        }

        private void BindProjects()
        {
            objSanction = new Sanction();
            DataTable dt = objSanction.BindProducts();
            ddlProjectCode.DataValueField = "ProjectCode";
            ddlProjectCode.DataTextField = "ProjectCode";
            ddlProjectCode.DataSource = dt;
            ddlProjectCode.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objSanction = new Sanction();
            objSanction.ProjectCode = ddlProjectCode.SelectedValue;
            objSanction.FairAmount = Convert.ToDecimal(txtFairAmount.Text);
            objSanction.HotelCharges = Convert.ToDecimal(txtHotelCharges.Text);
            objSanction.LocalConveyance = Convert.ToDecimal(txtLocalConv.Text);
            DataTable dtOpeningBalnce = objSanction.GetOpeningBalanceByProjectCode();
            DataTable dtTotalAmount = objSanction.GetTotalAmountByProjectCode(ddlProjectCode.SelectedValue);
            if (btnSave.Text == "Submit")
            {
                objSanction.Activity = "Addition";
                objSanction.TotalAmount = Convert.ToDecimal(Convert.ToDecimal(txtFairAmount.Text) + Convert.ToDecimal(txtHotelCharges.Text) + Convert.ToDecimal(txtLocalConv.Text));
                if (dtTotalAmount.Rows[0][0].ToString() != "")
                {
                    if (Convert.ToDecimal(dtOpeningBalnce.Rows[0][0]) >= (Convert.ToDecimal(dtTotalAmount.Rows[0][0]) + objSanction.TotalAmount))
                    {
                        int ReqNo = objSanction.SaveTABill();
                        objSanction.UpdateConsumption(Convert.ToDecimal(dtTotalAmount.Rows[0][0]) + objSanction.TotalAmount);
                        BindBillDetails(ReqNo);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Can not be processes as Balance is less than your Total Amount')", true);
                    }
                }
                else if (Convert.ToDecimal(dtOpeningBalnce.Rows[0][0]) >= objSanction.TotalAmount)
                {
                    int ReqNo = objSanction.SaveTABill();
                    objSanction.UpdateConsumption(objSanction.TotalAmount);
                    BindBillDetails(ReqNo);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Can not be processes as Balance is less than your Total Amount')", true);
                }
            }
            else
            {
                objSanction.RequestNumber = Convert.ToInt32(txtRequestNo.Text.Trim());
                objSanction.Activity = "Updation";
                objSanction.TotalAmount = Convert.ToDecimal(Convert.ToDecimal(txtFairAmount.Text) + Convert.ToDecimal(txtHotelCharges.Text) + Convert.ToDecimal(txtLocalConv.Text));
               
                DataTable dtAmount = objSanction.GetTotalAmount(Convert.ToInt32(txtRequestNo.Text.Trim()));
                decimal oldTotalAmount = Convert.ToDecimal(dtAmount.Rows[0][0]);
                decimal newTotalAmount = Convert.ToDecimal(Convert.ToDecimal(txtFairAmount.Text) + Convert.ToDecimal(txtHotelCharges.Text) + Convert.ToDecimal(txtLocalConv.Text));
                if (newTotalAmount > oldTotalAmount)
                {
                    objSanction.UpdatedTotalAmount = newTotalAmount - oldTotalAmount;
                }
                else
                {
                    objSanction.UpdatedTotalAmount = decimal.Negate(oldTotalAmount - newTotalAmount);
                }

                if (Convert.ToDecimal(dtOpeningBalnce.Rows[0][0]) >= (Convert.ToDecimal(dtTotalAmount.Rows[0][0]) + objSanction.UpdatedTotalAmount))
                {
                    int res = objSanction.UpdateTABill();
                    objSanction.SaveTABillHistory();
                    objSanction.UpdateConsumption(Convert.ToDecimal(Convert.ToDecimal(dtTotalAmount.Rows[0][0]) + objSanction.UpdatedTotalAmount));
                    BindBillDetails(Convert.ToInt32(Request.QueryString["RequestNumber"]));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Can not be processes as Opening Balance is less than your Total Amount')", true);
                }
            }
        }

        protected void BindBillDetails(int ReqNo)
        {
            objSanction = new Sanction();
            DataTable dt;

            dt = objSanction.GetSanctionBillsBySerial(ReqNo);
            txtRequestNo.Text = dt.Rows[0]["RequestNumber"].ToString();
            txtFairAmount.Text = dt.Rows[0]["FairAmount"].ToString();
            txtHotelCharges.Text = dt.Rows[0]["HotelCharges"].ToString();
            txtLocalConv.Text = dt.Rows[0]["LocalConveyance"].ToString();
            ddlProjectCode.SelectedValue = dt.Rows[0]["ProjectCode"].ToString();

        }
    }
}