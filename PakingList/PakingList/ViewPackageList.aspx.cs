using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using System.Data;
using System.IO;

namespace PakingList
{
    public partial class ViewPackageList : System.Web.UI.Page
    {
        Product objProduct;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  ShowData();
                    spRecNo.InnerHtml = Request.QueryString["RcNo"];
                    spPoNo.InnerHtml = Request.QueryString["PoNo"];
                    BindPakingListNo();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue');", true);
            }
        }

        private void BindPakingListNo()
        {
            objProduct = new Product();
            objProduct.ISGECPoNo = Request.QueryString["PoNo"];
            DataTable dtPKNO = objProduct.GetPakingListNO(Request.QueryString["CmpId"], Request.QueryString["env"]);
            ddlPKNo.DataSource = dtPKNO;
            ddlPKNo.DataTextField = "Invoice";
            ddlPKNo.DataValueField = "t_pkno";
            ddlPKNo.DataBind();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                objProduct = new Product();
                objProduct.ReceiptNumber = Request.QueryString["RcNo"];
                objProduct.ISGECPoNo = Request.QueryString["PoNo"];
                objProduct.PkNo = Convert.ToInt32(ddlPKNo.SelectedValue);
                DataTable dtProduct = objProduct.GetProducts(Request.QueryString["CmpId"], Request.QueryString["env"]);

                if (dtProduct.Rows.Count > 0)
                {
                    //spReceiptNo.InnerHtml = dtProduct.Rows[0]["ReceiptNumber"].ToString();
                    //spIsgecPONo.InnerHtml = dtProduct.Rows[0]["ISGECPoNo"].ToString();
                    spInvoiceNo.InnerHtml = dtProduct.Rows[0]["InvoiceNumber"].ToString();
                    spPackingDate.InnerHtml = Convert.ToDateTime(dtProduct.Rows[0]["PackingListDate"].ToString()).ToString("dd-MM-yyyy");
                    spNetWeight.InnerHtml = dtProduct.Rows[0]["NetWeight"].ToString();
                    spTransporterName.InnerHtml = dtProduct.Rows[0]["TranspoterName"].ToString();
                    spVehicleNo.InnerHtml = dtProduct.Rows[0]["vehicleNumber"].ToString();
                    spLRNo.InnerHtml = dtProduct.Rows[0]["LRNumber"].ToString();
                    spLRDate.InnerHtml = Convert.ToDateTime(dtProduct.Rows[0]["LRDate"].ToString()).ToString("dd-MM-yyyy");

                    DataTable dtDetails = objProduct.GetProductDetails(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    gvProductDetails.DataSource = dtDetails;
                    gvProductDetails.DataBind();
                    divNorecord.Visible = false;
                    divProducts.Visible = true;
                    btnPrint.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    divNorecord.Visible = true;
                    divProducts.Visible = false;
                    btnPrint.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue');", true);
            }
        }

        protected void ShowData()
        {
            try
            {
                objProduct = new Product();
                objProduct.ReceiptNumber = Request.QueryString["RcNo"];
                objProduct.ISGECPoNo = Request.QueryString["PoNo"];

                DataTable dtProduct = objProduct.GetProducts(Request.QueryString["CmpId"], Request.QueryString["env"]);

                if (dtProduct.Rows.Count > 0)
                {
                    //spReceiptNo.InnerHtml = dtProduct.Rows[0]["ReceiptNumber"].ToString();
                    //spIsgecPONo.InnerHtml = dtProduct.Rows[0]["ISGECPoNo"].ToString();
                    spInvoiceNo.InnerHtml = dtProduct.Rows[0]["InvoiceNumber"].ToString();
                    spPackingDate.InnerHtml = Convert.ToDateTime(dtProduct.Rows[0]["PackingListDate"].ToString()).ToString("dd-MM-yyyy");
                    spNetWeight.InnerHtml = dtProduct.Rows[0]["NetWeight"].ToString();
                    spTransporterName.InnerHtml = dtProduct.Rows[0]["TranspoterName"].ToString();
                    spVehicleNo.InnerHtml = dtProduct.Rows[0]["vehicleNumber"].ToString();
                    spLRNo.InnerHtml = dtProduct.Rows[0]["LRNumber"].ToString();
                    spLRDate.InnerHtml = Convert.ToDateTime(dtProduct.Rows[0]["LRDate"].ToString()).ToString("dd-MM-yyyy");

                    DataTable dtDetails = objProduct.GetProductDetails(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    gvProductDetails.DataSource = dtDetails;
                    gvProductDetails.DataBind();
                    divNorecord.Visible = false;
                    divProducts.Visible = true;
                    btnPrint.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    divNorecord.Visible = true;
                    divProducts.Visible = false;
                    btnPrint.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                string IsgecItemCode = string.Empty;
                string IsgecItemCodeQuant = string.Empty;
                objProduct = new Product();
                //DataTable dtStatus = objProduct.GetReceiptStatus(spReceiptNo.InnerHtml, Request.QueryString["CmpId"], Request.QueryString["env"]);
                //if (dtStatus.Rows[0][0].ToString() != "20")
                //{
                for (int i = 0; i <= gvProductDetails.Rows.Count - 1; i++)
                {
                    objProduct = new Product();
                    objProduct.ReceiptNumber = Request.QueryString["RcNo"];
                    objProduct.ISGECItemCode = gvProductDetails.DataKeys[i].Values[1].ToString();
                    objProduct.ISGECPoNo = Request.QueryString["PoNo"];
                    objProduct.PkNo =Convert.ToInt32(ddlPKNo.SelectedValue);
                    DataTable dt = objProduct.GetRecNoToUpdate(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtQ = objProduct.GetTotalQuantyByOrdr(dt.Rows[0]["t_orno"].ToString(), Request.QueryString["CmpId"], Request.QueryString["env"]);
                        if (dtQ.Rows.Count > 0)
                        {
                            if ((Convert.ToDecimal(dtQ.Rows[0][0])) + Convert.ToDecimal(gvProductDetails.DataKeys[i].Values[2].ToString()) <= Convert.ToDecimal(dt.Rows[0]["t_qnty"]))
                            {
                                objProduct.Quantity = Convert.ToDecimal(gvProductDetails.DataKeys[i].Values[2].ToString());
                                objProduct.TotalWeight = (objProduct.Quantity * Convert.ToDecimal(dt.Rows[0]["t_wght"])) / Convert.ToDecimal(dt.Rows[0]["t_qnty"]);
                                res = objProduct.UpdatePackage(Request.QueryString["CmpId"], Request.QueryString["env"]);
                            }
                            else
                            {
                                IsgecItemCodeQuant += objProduct.ISGECItemCode + "  " + "  -   Packing Quantity(" + (Convert.ToDecimal(dtQ.Rows[0][0]) + Convert.ToDecimal(gvProductDetails.DataKeys[i].Values[2].ToString())).ToString() + ")" + "  ,  PO Quantity(" + dt.Rows[0]["t_qnty"].ToString() + ")" + "<br />";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Receipt quantity should not be greater than ordered quantity');", true);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        IsgecItemCode += objProduct.ISGECItemCode + "<br />";
                    }
                }

                if (IsgecItemCode != string.Empty)
                {
                    stRcno.InnerHtml = objProduct.ReceiptNumber;
                    spItemCode.InnerHtml = IsgecItemCode;
                    divMatchItem.Visible = true;
                    mpeShowIsgecitemCode.Show();
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('"+IsgecItemCode+"');", true);
                }
                else
                {
                    divMatchItem.Visible = false;
                }
                if (IsgecItemCodeQuant != string.Empty)
                {
                    divMatchQuant.Visible = true;
                    stRcno.InnerHtml = objProduct.ReceiptNumber;
                    spItemCodeQuant.InnerHtml = IsgecItemCodeQuant;
                    mpeShowIsgecitemCode.Show();
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('"+IsgecItemCode+"');", true);
                }
                else
                {
                    divMatchQuant.Visible = false;
                }
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Updated');", true);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Receipt is already confirmed');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue');", true);
            }
        }


    }
}