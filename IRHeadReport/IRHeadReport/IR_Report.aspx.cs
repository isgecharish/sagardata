using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IRHeadReport
{
    public partial class IR_Report : System.Web.UI.Page
    {
        ReportClass objReportcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDetails();
        }

        private void BindDetails()
        {
            try
            {
                if (Request.QueryString["CmpId"] != null && Request.QueryString["env"] != null && Request.QueryString["IRNo"] != null)
                {
                    objReportcls = new ReportClass();
                    string Con = objReportcls.Environment(Request.QueryString["env"]);
                    objReportcls.IRNo = Request.QueryString["IRNo"];
                    DataTable dtHeader = objReportcls.GetIRData(Request.QueryString["CmpId"], Con);
                    DataTable dtCargoType = objReportcls.GetCargoType150();
                    if (dtHeader.Rows.Count > 0)
                    {
                        spIrNo.InnerHtml = dtHeader.Rows[0]["IRNO"].ToString();
                        spIRNDate.InnerHtml = Convert.ToDateTime(dtHeader.Rows[0]["IrDate"]).ToString("dd-MM-yyyy");
                        spIRAmount.InnerHtml = Convert.ToDecimal(dtHeader.Rows[0]["IRAmount"]).ToString("0.00");
                        spBussinessPartner.InnerHtml = dtHeader.Rows[0]["BussinessPartner"].ToString();
                        spName.InnerHtml = dtHeader.Rows[0]["Name"].ToString();
                        spRecNo.InnerHtml = dtHeader.Rows[0]["RecNo"].ToString();
                        spPurAmount.InnerHtml = Convert.ToDecimal(dtHeader.Rows[0]["PurAmount"]).ToString("0.00");
                        spInvoiceNo.InnerHtml = dtHeader.Rows[0]["SupInvoice"].ToString();
                        spSupplierDate.InnerHtml = Convert.ToDateTime(dtHeader.Rows[0]["SupDAte"]).ToString("dd-MM-yyyy");
                        spOrderNo.InnerHtml = dtHeader.Rows[0]["PurchagrOrNo"].ToString();
                        spProjectCode.InnerHtml = dtHeader.Rows[0]["ProjectCode"].ToString();
                        spDescription.InnerHtml = dtHeader.Rows[0]["ProjectDesc"].ToString();
                        if (dtHeader.Rows.Count > 0)
                        {
                            spCargoType.InnerHtml = dtCargoType.Rows[0]["CargoType"].ToString();
                            spShippingName.InnerHtml = dtCargoType.Rows[0]["ShippingLineName"].ToString();
                            spMBLNo.InnerHtml = dtCargoType.Rows[0]["MBLNo"].ToString();
                            spBLNO.InnerHtml = dtCargoType.Rows[0]["BLNumber"].ToString();
                            spBLNo1.InnerHtml = dtCargoType.Rows[0]["BLNumber"].ToString();
                        }
                    }

                    DataTable dtIRDetails = objReportcls.GetIrDetails150();
                    if (dtIRDetails.Rows.Count > 0)
                    {
                        gvIrDetails.DataSource = dtIRDetails;
                        gvIrDetails.DataBind();

                        spTotalBAsicAmount.InnerHtml = dtIRDetails.Compute("Sum(" + dtIRDetails.Columns[3].ColumnName + ")", "").ToString();
                    }

                    DataTable dtIRDATABLID = objReportcls.GetIrDetailsByBLID150();
                    if (dtIRDATABLID.Rows.Count > 0)
                    {
                        gvIRDetailsRefNo.DataSource = dtIRDATABLID;
                        gvIRDetailsRefNo.DataBind();
                        spTotalBAsicAmountDetails.InnerHtml = dtIRDATABLID.Compute("Sum(" + dtIRDATABLID.Columns[3].ColumnName + ")", "").ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Record not found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
    }
}