using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

namespace PakingList
{
    public partial class UploadPackage : System.Web.UI.Page
    {
        IExcelDataReader excelReader;
        Product objProduct;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                string IsgecItemCode = string.Empty;
                if (fileUpload.HasFile)
                {
                    string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["ExcelFile"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileUpload.SaveAs(FilePath);
                    FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
                    if (Extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);      // Reading from a binary Excel file ('97-2003 format; *.xls)
                    }
                    else if (Extension == ".xlsx")
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);     // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Format Should be in xls or xlsx.');", true);
                    }
                    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                    DataSet ds = excelReader.AsDataSet();
                    DataTable dt = ds.Tables[0];
                    string value = string.Empty;

                    foreach (DataRow dr in dt.Rows)
                    {
                        string Header = dr[0].ToString();
                        if (value == string.Empty)
                        {
                            value = dr[1].ToString() + ",";
                        }
                        else
                        {
                            value += dr[1].ToString() + ",";
                        }
                    }
                    string[] Values = value.Split(',');
                    objProduct = new Product();
                    objProduct.ReceiptNumber = Values[1].Trim();
                    objProduct.ISGECPoNo = Values[2].Trim();
                    objProduct.InvoiceNumber = Values[3].Trim();
                    objProduct.PackingListDate = Convert.ToDateTime(Values[4].Trim()).ToString("yyyy-MM-dd hh:mm:ss");
                    objProduct.NetWeight = Values[5] != "" ? Convert.ToDecimal(Values[5]) : 0;
                    objProduct.GrossWeight = Values[6] != "" ? Convert.ToDecimal(Values[6]) : 0;
                    objProduct.TranspoterName = Values[7].Trim();
                    objProduct.vehicleNumber = Values[8].Trim();
                    objProduct.LRNumber = Values[9].Trim();
                    objProduct.LRDate = Convert.ToDateTime(Values[10].Trim()).ToString("yyyy-MM-dd hh:mm:ss");

                    //DataTable dtStatus = objProduct.GetReceiptStatus(objProduct.ReceiptNumber, Request.QueryString["CmpId"], Request.QueryString["env"]);
                    //if (dtStatus.Rows[0][0].ToString() != "20")
                    //{
                    //DataTable dtRcNo = objProduct.GetRecNoFrom(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    //if (dtRcNo.Rows[0][0].ToString() != "0")
                    //{
                    objProduct.DeleteProduct(Request.QueryString["CmpId"], Request.QueryString["env"]); // Delete Product
                    DataTable dtPkNo = objProduct.GetPKNO(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    objProduct.PkNo = Convert.ToInt32(dtPkNo.Rows[0][0]);
                    int result = objProduct.InsertProduct(Request.QueryString["CmpId"], Request.QueryString["env"]);
                    if (result > 0)
                    {
                        DataTable dtPackage = objProduct.CreateTableForPackageDetails();
                        for (int i = 14; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][1].ToString() != "")
                            {
                                DataRow dr = dtPackage.NewRow();
                                dr["PkNumber"] = objProduct.PkNo;
                                dr["OrderNumber"] = objProduct.ISGECPoNo;
                                dr["SerialNumber"] = dt.Rows[i][0].ToString() != "" ? Convert.ToInt32(dt.Rows[i][0].ToString()) : 0;
                                dr["ReceiptNumber"] = objProduct.ReceiptNumber;
                                dr["ISGECItemCode"] = dt.Rows[i][1].ToString();
                                dr["UOM"] = dt.Rows[i][3].ToString();
                                dr["Quantity"] = dt.Rows[i][4].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][4].ToString()) : 0;
                                dr["UnitWeight"] = dt.Rows[i][5].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][5].ToString()) : 0;
                                dr["TotalWeight"] = dt.Rows[i][6].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][6].ToString()) : 0;
                                decimal  Calwt= Math.Round(Convert.ToDecimal(dt.Rows[i][4]) *Convert.ToDecimal(dt.Rows[i][5]),2);
                                decimal OrigTotalWt = Math.Round(Convert.ToDecimal(dt.Rows[i][6]), 2);
                                decimal TotalWtDiff = OrigTotalWt - Calwt;
                                if (Math.Abs(TotalWtDiff) > 1)
                                {
                                    IsgecItemCode += dt.Rows[i][1].ToString() + "<br />";
                                }
                                dr["DrawingId"] = dt.Rows[i][7].ToString();
                                dr["RevisionNumber"] = dt.Rows[i][8].ToString();
                                dr["PackageType"] = dt.Rows[i][9].ToString();
                                dr["PackageMarks"] = dt.Rows[i][10].ToString();
                                dr["Length"] = dt.Rows[i][11].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][11].ToString()) : 0;
                                dr["Width"] = dt.Rows[i][12].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][12].ToString()) : 0;
                                dr["Height"] = dt.Rows[i][13].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][13].ToString()) : 0;
                                dr["UOMDimension"] = dt.Rows[i][14].ToString();
                                dr["Refcntd"] = "0";
                                dr["Refcntu"] = "0";
                                dtPackage.Rows.Add(dr);
                            }
                        }

                        if (IsgecItemCode != string.Empty)
                        {
                            stRcno.InnerHtml = objProduct.PkNo.ToString();
                            spItemCode.InnerHtml = IsgecItemCode;
                            objProduct.DeleteProduct(Request.QueryString["CmpId"], Request.QueryString["env"]); // Delete Product
                            mpeShowIsgecitemCode.Show();
                        }
                        else
                        {
                            res = objProduct.InsertProductDetails(dtPackage, Request.QueryString["CmpId"], Request.QueryString["env"]);
                            if (res > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Invalid Receipt Number');", true);
                    //}
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Receipt is already confirmed');", true);
                    //}
                    excelReader.Close();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue Data Not Uploaded');", true);
            }
        }
    }
}