using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Net;

namespace VendorDocument
{
    public partial class VendorDocument : System.Web.UI.Page
    {
        Vendor objVendor;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    if (Request.QueryString["ReceiptNo"] != null && Request.QueryString["RevisionNo"] != null)
                    {
                        BindVendor();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void BindVendor()
        {
            objVendor = new Vendor();
            DataTable dt = objVendor.GetVendordetails(Request.QueryString["ReceiptNo"], Request.QueryString["RevisionNo"]);
            if (dt.Rows.Count > 0)
            {
                gvVandorDoc.DataSource = dt;
                gvVandorDoc.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found');", true);
            }
        }
        protected void lnkOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                //string Path = "~/Untitled Document.pdf";// lnkbtn.CommandArgument;
                //string Path1=lnkbtn.CommandArgument;
                //Response.Redirect(Path, true);

                string[] value = lnkbtn.CommandArgument.Split('&');
                string FileNameExtension = System.IO.Path.GetExtension(value[1]);
                //   string PathExtension = System.IO.Path.GetExtension(value[0]);
              //  string path = System.IO.Path.ChangeExtension(value[0], FileNameExtension);
                string FileName = System.IO.Path.GetFileNameWithoutExtension(value[1]);
                string File = value[0];
                System.Net.WebClient client = new System.Net.WebClient();

                if (File != "")
                {
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + FileNameExtension+ "\"");
                    byte[] data = req.DownloadData(File);
                    response.BinaryWrite(data);
                    response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Path not found');", true);
                }
                #region change content type
                //Byte[] buffer = client.DownloadData(path);

                //if (buffer != null)
                //{
                //    if (Extension == ".xls" || Extension == ".xlsx")
                //    {
                //        Response.ContentType = "application/vnd.ms-excel";
                //        Response.AddHeader("content-Disposition", "inline; filename=" + path);
                //        Response.BinaryWrite(buffer);
                //    }
                //    else if (Extension == ".pdf")
                //    {
                //        Response.ContentType = "application/pdf";
                //        Response.AddHeader("content-Length", buffer.Length.ToString());
                //        Response.BinaryWrite(buffer);
                //    }
                //    else if (Extension == ".doc" || Extension == ".docx")
                //    {
                //        Response.ContentType = "application/vnd.ms-works";
                //    }
                //    else if (Extension == ".gif" || Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".tif" || Extension == ".bmp")
                //    {
                //        Response.ContentType = "image/" + Extension + "";
                //    }
                //    else if (Extension == ".pot" || Extension == ".ppt" || Extension == ".pps" || Extension == ".pptx" || Extension == ".ppsx")
                //    {
                //        Response.ContentType = "application/vnd.ms-powerpoint";
                //    }
                //    else if (Extension == "htm" || Extension == ".html")
                //    {
                //        Response.ContentType = "text/HTML";
                //    }
                //    else if (Extension == ".txt")
                //    {
                //        Response.ContentType = "text/plain";
                //    }
                //    else if (Extension == ".zip")
                //    {
                //        Response.ContentType = "application/zip";
                //    }
                //    else if (Extension == ".rar" || Extension == ".tar" || Extension == ".tgz")
                //    {
                //        Response.ContentType = "text/x-compressed";
                //    }
                //    else
                //    {
                //        Response.ContentType = "application/octet-stream";
                //    }
                //}

                #endregion

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Path not found');", true);
            }
        }


    }
}