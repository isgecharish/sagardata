using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TABillSanctions
{
    public partial class UpdateBills : System.Web.UI.Page
    {
        Sanction objSanction;
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
                DataTable dt = objSanction.GetSanctionBills();
                gvBill.DataSource = dt;
                gvBill.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            Response.Redirect("BillForm.aspx?RequestNumber=" + lnkUpdate.CommandArgument, false);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDelete = (LinkButton)sender;
                string[] BillDetails = lnkDelete.CommandArgument.ToString().Split('$');
                objSanction = new Sanction();
                objSanction.RequestNumber = Convert.ToInt32(BillDetails[0]);
                objSanction.UpdatedTotalAmount = decimal.Negate(Convert.ToDecimal(BillDetails[1]));
                objSanction.ProjectCode = BillDetails[2];
                objSanction.Activity = "Deletion";
                objSanction.FairAmount = decimal.Negate(Convert.ToDecimal(BillDetails[3]));
                objSanction.HotelCharges = decimal.Negate(Convert.ToDecimal(BillDetails[4]));
                objSanction.LocalConveyance = decimal.Negate(Convert.ToDecimal(BillDetails[5]));
                DataTable dtConsumption = objSanction.GetTotalConsumption();
                decimal newConsumption = Convert.ToDecimal(dtConsumption.Rows[0][0]) - Convert.ToDecimal(BillDetails[1]);
                objSanction.DeleteSanction(Convert.ToInt32(BillDetails[0]));
                int res = objSanction.SaveTABillHistory();
                objSanction.UpdateConsumption(newConsumption);
                BindBills();
            }
            catch (Exception ex)
            {

            }
        }
    }
}