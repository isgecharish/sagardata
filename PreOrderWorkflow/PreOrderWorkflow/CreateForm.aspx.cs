﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Services;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace PreOrderWorkflow
{
    public partial class CreateForm : System.Web.UI.Page
    {
        WorkFlow objWorkFlow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["WFID"] != null && Request.QueryString["Status"] == "Technical Specification Released Returned")
                {
                    GetData();
                    btnSave.Text = "ReSubmit";
                    btnNotes.Visible = true;
                }
                if (Request.QueryString["WFID"] != null && Request.QueryString["Status"] == "Created")
                {
                    GetData();
                    btnSave.Text = "Release";
                }

                GetProject();
                GetProjectElement();
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
                string[] Project = dt.Rows[0]["Project"].ToString().Split('-');
                string[] Element = dt.Rows[0]["Element"].ToString().Split('-');
                ddlProject.SelectedValue = Project[0];
                lblProjectName.Text = Project[1];
                ddlElement.Text = Element[0];
                lblProjectElementName.Text = Element[1];
                // ddlSpecification.SelectedValue= dt.Rows[0]["SpecificationNo"].ToString();
                txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                // hdfStatus.Value = dt.Rows[0]["WF_Status"].ToString();
                // hdfWFID.Value = dt.Rows[0]["WFID"].ToString();
            }
            if (dtGetNotes.Rows.Count > 0)
            {
                txtNotes.Text = dtGetNotes.Rows[0]["Notes"].ToString();
            }
        }
        private void GetProject()
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProject();
            ddlProject.DataSource = dt;
            ddlProject.DataValueField = "ProjectCode";
            ddlProject.DataTextField = "ProjectCode";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "Select");
        }
        private void GetProjectElement()
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProjectElement(ddlProject.SelectedValue);
            ddlElement.DataSource = dt;
            ddlElement.DataValueField = "Element";
            ddlElement.DataTextField = "Element";
            ddlElement.DataBind();
            ddlElement.Items.Insert(0, "Select");
        }
        private void GetSpecification()
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProjectSpecification(ddlProject.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                //ddlSpecification.DataSource = dt;
                //ddlSpecification.DataValueField = "DocumentID";
                //ddlSpecification.DataTextField = "DocumentID";
                //ddlSpecification.DataBind();
                //ddlSpecification.Items.Insert(0, "Select");
                //txtSpecification.Visible = false;
                //ddlSpecification.Visible = true;
                //lblSpecDesc.Visible = true;
            }
            else
            {
                txtSpecification.Visible = true;
                //  ddlSpecification.Visible = false;
                lblSpecDesc.Visible = false;
            }
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProjectName(ddlProject.SelectedValue);
            lblProjectName.Text = dt.Rows[0][1].ToString();
            GetProjectElement();
            // GetSpecification();
            txtSpecification.Text = "";
        }
        protected void ddlElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProjectElementFDesc(ddlProject.SelectedValue, ddlElement.SelectedValue);
            lblProjectElementName.Text = dt.Rows[0][1].ToString();
        }

        //protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //objWorkFlow = new WorkFlow();
        //    //DataTable dt = objWorkFlow.GetProjectSpecificationDesc(ddlProject.SelectedValue, ddlSpecification.SelectedValue);
        //    //lblSpecDesc.Text = dt.Rows[0]["DocumentDescription"].ToString();
        //}

        [WebMethod]
        public static string[] GetSpecificationMethod(string prefixText, int count)
        {
            WorkFlow objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProjectSpecificationMethod(prefixText);
            List<string> lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string Specification = dr["DocumentID"].ToString() + " (" + dr["DocumentDescription"].ToString() + ")";
                lst.Add(Specification);
            }
            return lst.ToArray();
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
        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["WFID"] != null)
                {
                    string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFID"] + "&AttachedBy=" + Request.QueryString["u"] + "&ed=a";
                    string s = "window.open('" + url + "','abc','width=800,height=600,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {
                    if (ddlProject.SelectedValue != "Select" && ddlElement.SelectedValue != "Select" && txtBuyer.Text != "" && txtSpecification.Text != "")
                    {
                        string[] Buyer = txtBuyer.Text.Split('-');
                        objWorkFlow = new WorkFlow();
                        objWorkFlow.Parent_WFID = 0;
                        objWorkFlow.Project = ddlProject.SelectedValue + "-" + lblProjectName.Text;
                        objWorkFlow.Element = ddlElement.SelectedValue + "-" + lblProjectElementName.Text;
                        //  objWorkFlow.SpecificationNo = txtSpecification.Text != "" ? txtSpecification.Text.Trim() : ddlSpecification.SelectedValue + "-" + lblSpecDesc.Text;
                        objWorkFlow.SpecificationNo = txtSpecification.Text.Trim();
                        objWorkFlow.Buyer = Buyer[1];
                        objWorkFlow.UserId = Request.QueryString["u"];
                        objWorkFlow.WF_Status = "Created";

                        DataTable dtres = objWorkFlow.InsertPreOrderData();
                        hdfWFID.Value = dtres.Rows[0][0].ToString();
                        if (dtres.Rows.Count > 0)
                        {
                            //Insert In History
                            objWorkFlow.WFID = Convert.ToInt32(dtres.Rows[0][0]);
                            objWorkFlow.Supplier = "";
                            objWorkFlow.SupplierName = "";
                            objWorkFlow.Notes = txtNotes.Text;
                            DataTable dtWFHID = objWorkFlow.InserPreOrderHistory();
                            string IndexValue = dtWFHID.Rows[0][0].ToString() + "_" + dtWFHID.Rows[0][1].ToString();
                            hdfHistoryID.Value = dtWFHID.Rows[0][1].ToString();
                            //  divAlert.Visible = true;
                            btnSave.Text = "Release";
                            //  Response.Redirect("ReleaseTechnicalSpecification.aspx?u=" + Request.QueryString["u"]);

                            /// Open Attachment Page
                            string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + hdfWFID.Value + "&AttachedBy=" + Request.QueryString["u"] + "&ed=a";
                            string s = "window.open('" + url + "','abc','width=800,height=600,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


                            //SAve Attachment
                            // if (dtWFHID.Rows.Count > 0)
                            // {
                            // string IndexValue = dtWFHID.Rows[0][0].ToString() + "_" + dtWFHID.Rows[0][1].ToString();
                            // UploadAttachments(IndexValue);
                            //  }

                            //Mail Send
                            //DataTable dtMailTo = objWorkFlow.GetMAilID(Buyer[1]);
                            // string MailTo = dtMailTo.Rows[0]["EMailid"].ToString();
                            // SendMail(MailTo);
                            //  ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Insert');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Fill All information');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        protected void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProject.SelectedValue != "Select" && ddlElement.SelectedValue != "Select" && txtBuyer.Text != "" && txtSpecification.Text != "")
                {
                    string[] Buyer = txtBuyer.Text.Split('-');
                    objWorkFlow = new WorkFlow();
                    objWorkFlow.Parent_WFID = 0;
                    objWorkFlow.Project = ddlProject.SelectedValue + "-" + lblProjectName.Text;
                    objWorkFlow.Element = ddlElement.SelectedValue + "-" + lblProjectElementName.Text;
                    //  objWorkFlow.SpecificationNo = txtSpecification.Text != "" ? txtSpecification.Text.Trim() : ddlSpecification.SelectedValue + "-" + lblSpecDesc.Text;
                    objWorkFlow.SpecificationNo = txtSpecification.Text.Trim();
                    objWorkFlow.Buyer = Buyer[1];
                    objWorkFlow.UserId = Request.QueryString["u"];
                    objWorkFlow.WF_Status = "Technical Specification Released";

                    if (btnSave.Text == "Save")
                    {
                        DataTable dtres = objWorkFlow.InsertPreOrderData();

                        if (dtres.Rows.Count > 0)
                        {
                            //Insert In History
                            objWorkFlow.WFID = Convert.ToInt32(dtres.Rows[0][0]);
                            objWorkFlow.Supplier = "";
                            objWorkFlow.SupplierName = "";
                            objWorkFlow.Notes = txtNotes.Text;
                            DataTable dtWFHID = objWorkFlow.InserPreOrderHistory();
                            string IndexValue = dtWFHID.Rows[0][0].ToString() + "_" + dtWFHID.Rows[0][1].ToString();

                            divAlert.Visible = true;
                            //Response.Redirect("ReleaseTechnicalSpecification.aspx?u=" + Request.QueryString["u"]);
                        }
                    }
                    if (btnSave.Text == "Release")
                    {
                        objWorkFlow = new WorkFlow();
                        objWorkFlow.WFID = Request.QueryString["WFID"] != null ? Convert.ToInt32(Request.QueryString["WFID"]) : Convert.ToInt32(hdfWFID.Value);
                        DataTable dtSlHistory = objWorkFlow.GetWFHID(Convert.ToInt32(objWorkFlow.WFID));
                        objWorkFlow.SLNO_WFID = Convert.ToInt32(dtSlHistory.Rows[0][0]);
                        objWorkFlow.WF_Status = "Technical Specification Released";
                        objWorkFlow.UpdateWF_Status();
                        objWorkFlow.UpdateStatusWFPreOrder_History();
                        // Response.Redirect("ReleaseTechnicalSpecification.aspx?u=" + Request.QueryString["u"]);
                    }
                    if (btnSave.Text == "ReSubmit")
                    {
                       // objWorkFlow = new WorkFlow();
                        objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);                     
                        int res = objWorkFlow.UpdateWFPreOrder();
                        if (res > 0)
                        {
                            // DataTable dtWFHID = objWorkFlow.GetWFHID(Convert.ToInt32(Request.QueryString["WFID"]));
                            // int res1 = objWorkFlow.UpdateWFPreOrder_History();
                            string IndexValue = InsertPreHistory(Convert.ToInt32(Request.QueryString["WFID"]), "Technical Specification Released");

                            //SAve Attachment
                            // UploadAttachments(IndexValue);

                            divAlert.Visible = true;
                        }
                        else
                        {

                        }
                    }

                    //   Mail Send
                    DataTable dtMailTo = objWorkFlow.GetMAilID(Buyer[1]);
                    string MailTo = dtMailTo.Rows[0]["EMailid"].ToString();
                    SendMail(MailTo);

                    Response.Redirect("ReleaseTechnicalSpecification.aspx?u=" + Request.QueryString["u"]);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Fill All information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        private string InsertPreHistory(int Id, string status)
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Id;
            DataTable dt = objWorkFlow.GetWFById();
            objWorkFlow.Parent_WFID = 0;
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

        #region Attachment and mail
        //protected void UploadAttachments(string IndexValue)
        //{
        //    if (fileUpload.HasFile)
        //    {
        //        objWorkFlow = new WorkFlow();
        //        // objWorkFlow.IndexValue = Request.QueryString["Index"];
        //        objWorkFlow.AttachmentHandle = "J_PREORDER_WORKFLOW";
        //        DataTable dt = objWorkFlow.GetPath();
        //        if (dt.Rows.Count > 0)
        //        {
        //            //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
        //            string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//


        //            int filecount = 0;
        //            filecount = fileUpload.PostedFiles.Count();
        //            if (filecount > 0)
        //            {
        //                foreach (HttpPostedFile PostedFile in fileUpload.PostedFiles)
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
        //                                fileUpload.SaveAs(ServerPath + dtDocID.Rows[0][0]);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                            }
        //                            // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
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
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Path does not exist');", true);
        //        }
        //    }
        //    else
        //    {

        //    }
        //    // }
        //    // else
        //    // {
        //    //     ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found Properly');", true);
        //    // }
        //}
        #endregion

        public void SendMail(string MailTo)
        {
            try
            {
                // if (fileUpload.HasFile)
                // {
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
                mM.To.Add(UserMailId); //MailTo
                mM.Subject = "Technical Specification Released" + "-" + txtSpecification.Text;
                //  foreach (HttpPostedFile PostedFile in fileUpload.PostedFiles)
                // {
                //     string fileName = Path.GetFileName(PostedFile.FileName);
                //     Attachment myAttachment = new Attachment(fileUpload.FileContent, fileName);
                //     mM.Attachments.Add(myAttachment);
                //  }
                mM.IsBodyHtml = true;
                mM.Body = txtNotes.Text;
                mM.Body =mM.Body.ToString().Replace("\n", "<br />");
                mM.Body += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />This mail has been triggered to draw your attention on the respective ERP/Joomla module. Please login to respective module to see further details and file attachments";
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
                //  }
                //   else
                //   {

                //  }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
            //}
        }

        protected void btnNotes_Click(object sender, EventArgs e)
        {
            string MailTo = string.Empty;
            if (Request.QueryString["WFID"] != null)
            {
                //Header
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

                if (Request.QueryString["p"] != null && Request.QueryString["p"] == "U")
                {
                    string[] Buyer = txtBuyer.Text.Split('-');
                    objWorkFlow = new WorkFlow();
                    DataTable dtMailTo = objWorkFlow.GetMAilID(Buyer[1]);
                    MailTo = dtMailTo.Rows[0]["EMailid"].ToString();
                }

                string url = "http://192.9.200.146/Attachment/Notes.aspx?handle=J_PREORDER_WORKFLOW&Index=" + Request.QueryString["WFID"] + "&user=" + Request.QueryString["u"] + "&Em=" + MailTo + "&Hd=" + Header + "&Tl=" + txtSpecification.Text;
                string s = "window.open('" + url + "','abc','width=1300,height=700,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
    }
}