﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PreOrderWorkflow
{
    public partial class RaiseEnquiryForm : System.Web.UI.Page
    {
        WorkFlow objWorkFlow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Status"] == "Enquiry Raised")
                {
                    btnSendEnquiry.Text = "Send Enquiry";
                    hHeader.InnerHtml = "Send Enquiry";
                    
                }
                if (Request.QueryString["Status"] == "Technical offer Received")
                {
                    btnSendEnquiry.Text = "Technical offer received";
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    hHeader.InnerHtml = "View Enquiry";
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }
                if (Request.QueryString["Status"] == "Isgec Comment Submitted")
                {
                    btnSendEnquiry.Text = "Release comments";
                    hHeader.InnerHtml = "View Enquiry";
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }
                if (Request.QueryString["Status"] == "Commercial offer Received")
                {
                    btnSendEnquiry.Text = "Commercial offer Received";
                    hHeader.InnerHtml = "View Enquiry";
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }
                if (Request.QueryString["Status"] == "done")
                {
                    btnSendEnquiry.Visible = false;
                    hHeader.InnerHtml = "View Enquiry";
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }

                GetData();
            }
        }
        private void GetData()
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
            DataTable dt = objWorkFlow.GetWFById();

            if (dt.Rows.Count > 0)
            {
                txtProject.Text = dt.Rows[0]["Project"].ToString();
                txtElement.Text = dt.Rows[0]["Element"].ToString();
                txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                txtSupplierEmail.Text = dt.Rows[0]["Supplier"].ToString();
                txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
                hdfBuyerId.Value = dt.Rows[0]["Buyer"].ToString();
                hdfRandomNo.Value = dt.Rows[0]["RandomNo"].ToString();
            }
        }

        //[WebMethod]
        //public static string[] GetSupplier(string prefixText, int count)
        //{
        //    WorkFlow objWorkFlow = new WorkFlow();
        //    objWorkFlow.Supplier = prefixText;
        //    DataTable dt = objWorkFlow.GetSupplier();
        //    List<string> lst = new List<string>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        string Supplier = dr["SupplierName"].ToString() + "-" + dr["SupplierCode"].ToString();
        //        lst.Add(Supplier);
        //    }
        //    return lst.ToArray();
        //}
        protected void btnSendEnquiry_Click(object sender, EventArgs e)
        {
            string RandomNo=string.Empty;
            if (hdfRandomNo.Value == "")
            {
                 RandomNo = GetRandomAlphanumericString(8);
            }

            if (txtSupplier.Text != "" && txtSupplierEmail.Text != "")
            {
                string MailTo = string.Empty;
                //   string[] supplier = txtSupplier.Text.Split('-');
                objWorkFlow = new WorkFlow();
                objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
                objWorkFlow.WF_Status = Request.QueryString["Status"];
                objWorkFlow.UserId = Request.QueryString["u"];
                objWorkFlow.SupplierName = txtSupplier.Text;
                objWorkFlow.Supplier = txtSupplierEmail.Text.Trim();
                objWorkFlow.RandomNo = RandomNo!=""? RandomNo : hdfRandomNo.Value;
                int res = objWorkFlow.UpdateEnquiryRaised();
                if (res > 0)
                {
                    string IndexValue = InsertPreHistory(Convert.ToInt32(Request.QueryString["WFID"]), Request.QueryString["Status"]);
                    // UploadAttachments(IndexValue);
                    if (Request.QueryString["Status"] == "Enquiry Raised")
                    {
                        MailTo = txtSupplierEmail.Text.Trim();
                        SendMail(MailTo, RandomNo);
                       
                        string url = "http://192.9.200.146/webtools2/CreateExternalUser.aspx?LoginID=" + RandomNo + "&Password=" + RandomNo + "&UserName=" + txtSupplier.Text.Trim() + "&EMailID=" + txtSupplierEmail.Text.Trim();
                        HttpWebRequest rq = (HttpWebRequest)WebRequest.Create( new Uri(url));
                        rq.Method = "GET";
                        rq.ContentType = "application/json";
                        WebResponse rs = rq.GetResponse();
                        System.IO.Stream st = rs.GetResponseStream();
                        System.IO.StreamReader sr = new System.IO.StreamReader(st);
                        String strResponse = sr.ReadToEnd();
                        sr.Close();    
                    }
                    GetData();

                    if (Request.QueryString["Status"] == "Enquiry Raised")
                    {
                        Response.Redirect("EnquiryInProcess.aspx?u=" + Request.QueryString["u"]);
                    }
                    if (Request.QueryString["Status"] == "Technical offer Received" || Request.QueryString["Status"] == "Commercial offer Received")
                    {
                        Response.Redirect("RaisedEnquiry.aspx?u=" + Request.QueryString["u"]+ "&WFPID=" + Request.QueryString["WFPID"]);
                    }
                    if (Request.QueryString["Status"] == "Isgec Comment Submitted")
                    {
                        Response.Redirect("ReleaseComments.aspx?u=" + Request.QueryString["u"]);
                    }
                    // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Request.QueryString["Status"] + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Fill All information');", true);
            }
            //
            //send mail
        }
        private string InsertPreHistory(int Id, string status)
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
            objWorkFlow.Notes = txtNotes.Text;
            DataTable dtWFHID = objWorkFlow.InserPreOrderHistory();

            string IndexValue = dtWFHID.Rows[0][0].ToString() + "_" + dtWFHID.Rows[0][1].ToString();
            return IndexValue;
        }
        #region Attachment
        //protected void UploadAttachments(string IndexValue)
        //{
        //    // if (Request.QueryString["AthHandle"] != null)
        //    //  {
        //    objWorkFlow = new WorkFlow();
        //    // objWorkFlow.IndexValue = Request.QueryString["Index"];
        //    objWorkFlow.AttachmentHandle = "J_PREORDER_WORKFLOW";
        //    DataTable dt = objWorkFlow.GetPath();
        //    if (dt.Rows.Count > 0)
        //    {
        //        //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
        //        string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//

        //        if (fileAttachment.HasFile)
        //        {
        //            int filecount = 0;
        //            filecount = fileAttachment.PostedFiles.Count();
        //            if (filecount > 0)
        //            {
        //                foreach (HttpPostedFile PostedFile in fileAttachment.PostedFiles)
        //                {
        //                    string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
        //                    string fileExtension = Path.GetExtension(PostedFile.FileName);
        //                    try
        //                    {
        //                        objWorkFlow = new WorkFlow();
        //                        objWorkFlow.AttachmentHandle = "J_PREORDER_WORKFLOW";
        //                        objWorkFlow.IndexValue = IndexValue;
        //                        objWorkFlow.PurposeCode = "PreOrderWorkFlow";// Request.QueryString["PurposeCode"];
        //                        objWorkFlow.AttachedBy = Request.QueryString["u"];
        //                        objWorkFlow.FileName = fileName + fileExtension;
        //                        objWorkFlow.LibraryCode = dt.Rows[0]["LibCode"].ToString();
        //                        // DataTable dtFile = objWorkFlow.GetFileName();
        //                        //  if (dtFile.Rows.Count == 0)
        //                        //  {
        //                        DataTable dtDocID = objWorkFlow.InsertAttachmentdata();
        //                        if (dtDocID.Rows[0][0].ToString() != "0")
        //                        {
        //                            try
        //                            {
        //                                fileAttachment.SaveAs(ServerPath + dtDocID.Rows[0][0]);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                            }
        //                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
        //                        }
        //                        else
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
        //                        }
        //                        //  }
        //                        //  else
        //                        //  {
        //                        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('This file name already exist please change your file name');", true);
        //                        // }
        //                    }
        //                    catch (System.Exception ex)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                    }
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Path does not exist');", true);
        //    }
        //    // }
        //    // else
        //    // {
        //    //     ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found Properly');", true);
        //    // }
        //}
        #endregion
        public void SendMail(string MailTo,string UserRandomNo)
        {
            try
            {
                //  if (fileAttachment.HasFile)
                //  {
                objWorkFlow = new WorkFlow();
                DataTable dtMailTo = objWorkFlow.GetMAilID(Request.QueryString["u"]);
                string UserMailId = dtMailTo.Rows[0]["EMailid"].ToString();
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress(UserMailId);
                // mM.To.Add(txtTo.Text.Trim());
                // string[] MailTo = txtTo.Text.Split(';');
                // foreach (string Mailid in MailTo)
                // {
                //     mM.To.Add(new MailAddress(Mailid));
                //  }
                mM.To.Add(MailTo); //MailTo
                mM.To.Add(UserMailId);
                mM.Subject = Request.QueryString["Status"] + "-" + txtSpecification.Text;
                //foreach (HttpPostedFile PostedFile in fileAttachment.PostedFiles)
                //{
                //    string fileName = Path.GetFileName(PostedFile.FileName);
                //    Attachment myAttachment = new Attachment(fileAttachment.FileContent, fileName);
                //    mM.Attachments.Add(myAttachment);
                //}
                mM.Body = txtNotes.Text;
                mM.IsBodyHtml = true;
                mM.Body += "<br /><br /><br /><a href='http://cloud.isgec.co.in/SupWF/SupEnquiryResponseForm.aspx?user=" + UserRandomNo + "'>Vendor Portal for Response Submission </a>";
                mM.Body += "<br /> <br /> You are requested to access the Vendor Portal for all communication and exchange of documents.";
                mM.Body += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />This mail has been triggered to draw your attention on the respective ERP/Joomla module. Please login to respective module to see further details and file attachments";
                mM.Body = mM.Body.Replace("\n", "<br />");
                SmtpClient sC = new SmtpClient("192.9.200.214", 25);
                //   sC.Host = "192.9.200.214"; //"smtp-mail.outlook.com"// smtp.gmail.com
                //   sC.Port = 25; //587
                sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                sC.UseDefaultCredentials = false;
                sC.Credentials = new NetworkCredential("baansupport@isgec.co.in", "isgec");
                //sC.Credentials = new NetworkCredential("adskvaultadmin", "isgec@123");
                sC.EnableSsl = false;  // true
                sC.Timeout = 10000000;
                sC.Send(mM);
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
            //}
        }

        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["WFPID"] != null)
            {
                string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&ed=y";
                string s = "window.open('" + url + "','abc','width=800,height=600,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        protected void btnNotes_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["WFID"] != null)
            {
                string MailTo = string.Empty;
                MailTo = txtSupplierEmail.Text;

                string Header = string.Empty;
                if (txtSpecification.Text.Contains("("))
                {
                    string[] Heading = txtSpecification.Text.Split('(');
                    Header = Heading[0];
                }
                else
                {
                    Header = txtSpecification.Text;
                }
                
                string Title = txtSpecification.Text;

                string url = "http://192.9.200.146/Attachment/Notes.aspx?handle=J_PREORDER_WORKFLOW&Index=" + Request.QueryString["WFID"] + "&user=" + Request.QueryString["u"] + "&Em=" + MailTo + "&Hd=" + Header + "&Tl=" + Title;
                string s = "window.open('" + url + "','abc','width=1300,height=700,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }

        // Random No Genrate
        public string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }
        public string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}