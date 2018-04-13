using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PreOrderWorkflow_Supplier
{
    public partial class SupEnquiryResponseForm : System.Web.UI.Page
    {

        WorkFlow objWorkFlow; NotesClass objNotes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["user"] != null)
                {
                    GetData();
                }
            }
        }
        private void GetData()
        {
            try
            {
                objWorkFlow = new WorkFlow();
                objWorkFlow.RandomNo = Request.QueryString["user"];
                DataTable dt = objWorkFlow.GetWFById();

                if (dt.Rows.Count > 0)
                {
                    txtProject.Text = dt.Rows[0]["Project"].ToString();
                    txtElement.Text = dt.Rows[0]["Element"].ToString();
                    txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                    txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                    txtSupplierEmail.Text = dt.Rows[0]["Supplier"].ToString();
                    txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
                    txtNotes.Text = dt.Rows[0]["Notes"].ToString();
                    hdfWFID.Value = dt.Rows[0]["WFID"].ToString();
                    hdfPWFID.Value = dt.Rows[0]["Parent_WFID"].ToString();
                    //---
                    DataTable dtMailTo = objWorkFlow.GetMAilID(dt.Rows[0]["Buyer"].ToString());
                    string BuyrMail = dtMailTo.Rows[0]["EMailid"].ToString();
                    txtMailTo.Text = BuyrMail;

                    //----
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
                    //---
                    string Title = txtSpecification.Text;
                    txtTitle.Text = Title;

                    spIndex.InnerHtml = Header;


                    GetNotes();
                    BindData();
                    // hdfBuyerId.Value = dt.Rows[0]["Buyer"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }


        #region Notes
        private void GetNotes()
        {
            try
            {
                objNotes = new NotesClass();
                objNotes.NotesHandle = "J_PREORDER_WORKFLOW";
                objNotes.IndexValue = hdfWFID.Value;
                DataTable dt = objNotes.GetNotesFromASPNETUSer();
                if (dt.Rows.Count > 0)
                {
                    rptNotes.DataSource = dt;
                    rptNotes.DataBind();
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No record found');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void rptNotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("row"); //Where TD1 is the ID of the Table Cell
                    HiddenField hdfUser = (HiddenField)e.Item.FindControl("hdfUserID");
                    tr.Attributes.Add("style", "background-color:" + hdfUser.Value + ";");
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnSaveNotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text != "" && txtDescription.Text != "")
                {
                    objNotes = new NotesClass();
                    objNotes.NotesHandle = "J_PREORDER_WORKFLOW";
                    objNotes.IndexValue = hdfWFID.Value;
                    objNotes.Title = txtTitle.Text.Trim();
                    objNotes.Description = txtDescription.Text.Trim();
                    objNotes.User = Request.QueryString["user"];
                    objNotes.SendEmailTo = txtMailTo.Text;
                    objNotes.RemiderMailId = "";
                    objNotes.ReminderDateTime = "";
                    //objNotes.ReminderDateTime =txtDate.Text!=""? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") +" "+ txtTime.Text.Trim(): System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + txtTime.Text.Trim();

                    if (btnSaveNotes.Text == "Submit")
                    {
                        //if (hdfNewNoteId.Value == "")
                        //{
                        DataTable dtNotesID = objNotes.Insertdata();
                        if (dtNotesID.Rows[0][0].ToString() != "0")
                        {
                            if (txtMailTo.Text != "")
                            {
                                SendMAil();
                            }
                            UploadAttachment(dtNotesID.Rows[0][0].ToString());
                            txtTitle.Text = "";
                            txtDescription.Text = "";
                            GetNotes();

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Saved');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Notes Handle does not exist');", true);
                        }
                        //}
                        //else
                        //{
                        //    objNotes.NoteID = hdfNewNoteId.Value;
                        //    int res = objNotes.UpdateNotes();
                        //    if (txtMailTo.Text != "")
                        //    {
                        //        SendMAil();
                        //    }
                        //    if (res > 0)
                        //    {
                        //        txtTitle.Text = "";
                        //        txtDescription.Text = "";
                        //        hdfNewNoteId.Value = "";
                        //        GetNotes();
                        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Saved');", true);
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                        //    }
                        //}
                    }

                    // Update
                    else
                    {
                        objNotes.NoteID = hdfNoteId.Value;
                        int res = objNotes.UpdateNotes();
                        if (txtMailTo.Text != "")
                        {
                            SendMAil();
                        }
                        if (res > 0)
                        {
                            GetNotes();
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Updated');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Enter all fields');", true);
                }

            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not Saved');", true);
            }
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtn = (LinkButton)sender;
                string[] Value = lnkBtn.CommandArgument.Split('&');
                string uId = Value[0];
                hdfUser.Value = uId;
                hdfNoteId.Value = Value[1];
                objNotes = new NotesClass();
                objNotes.NoteID = Value[1];
                DataTable dt = objNotes.GetNotesByRunningId();
                txtMailTo.Text = dt.Rows[0]["SendEmailTo"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                //add color to desc and button
                txtDescription.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                btnNewNotes.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                //------
                //   txtMailIdReminder.Text = dt.Rows[0]["ReminderTo"].ToString();
                //  txtDate.Text = dt.Rows[0]["ReminderDateTime"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["ReminderDateTime"].ToString()).ToString("dd-MM-yyyy") : "";
                if (uId == Request.QueryString["user"])
                {
                    txtTitle.Enabled = true;
                    txtDescription.Enabled = true;
                    btnSaveNotes.Text = "Update";
                    btnSaveNotes.Enabled = true;
                    btnDeleteNotes.Enabled = true;
                    btnDeleteNotes.Visible = true;
                    txtMailTo.Enabled = true;
                }
                else
                {
                    txtTitle.Enabled = false;
                    txtDescription.Enabled = false;
                    btnSaveNotes.Enabled = false;
                    btnDeleteNotes.Enabled = false;
                    txtMailTo.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('You are not authorised to update records');", true);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not Update');", true);
            }
        }
        protected void btnNewNotes_Click(object sender, EventArgs e)
        {
            txtTitle.Enabled = true;
            txtDescription.Enabled = true;
            txtTitle.Text = "";
            txtDescription.Text = "";
            btnSaveNotes.Text = "Submit";
            btnDeleteNotes.Visible = false;
            btnSaveNotes.Enabled = true;
            txtMailTo.Text = Request.QueryString["Em"];
            txtTitle.Text = Request.QueryString["Tl"];
            spIndex.InnerHtml = Request.QueryString["Hd"];
            hdfNoteId.Value = "";
            hdfNewNoteId.Value = "";
        }
        protected void btnDeleteNotes_Click(object sender, EventArgs e)
        {
            try
            {
                objNotes = new NotesClass();
                objNotes.NoteID = hdfNoteId.Value;
                int res = objNotes.DeleteNotes();
                if (res > 0)
                {
                    txtTitle.Text = "";
                    txtDescription.Text = "";
                    GetNotes();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Deleted');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted ');", true);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue data Not deleted');", true);
            }
        }
        protected void SendMAil()
        {
            try
            {
                if (txtMailTo.Text != "")
                {
                    // objNotes = new NotesClass();
                    // objNotes.User = Request.QueryString["user"];
                    // DataTable dtUserMail = objNotes.GetEmployeeMAilID();

                    MailMessage mM = new MailMessage();
                    // mM.From = new MailAddress(dtUserMail.Rows[0]["EmailID"].ToString());
                    mM.From = new MailAddress(txtSupplierEmail.Text);

                    string[] MailTo = txtMailTo.Text.Split(';');
                    foreach (string Mailid in MailTo)
                    {
                        mM.To.Add(new MailAddress(Mailid));
                    }
                    foreach (HttpPostedFile PostedFile in FileUpload.PostedFiles)
                    {
                        string fileName = Path.GetFileName(PostedFile.FileName);
                        Attachment myAttachment = new Attachment(FileUpload.FileContent, fileName);
                        mM.Attachments.Add(myAttachment);
                    }
                    //mM.To.Add(dtUserMail.Rows[0]["EmailID"].ToString());
                    mM.Subject = txtTitle.Text.Trim() + "-" + spIndex.InnerHtml;
                    // string file = Server.MapPath("~/Files/") + hdfFile.Value;
                    // mM.Attachments.Add(new System.Net.Mail.Attachment(file));
                    mM.Body = txtDescription.Text.Trim();
                    mM.IsBodyHtml = true;
                    mM.Body = mM.Body.ToString().Replace("\n", "<br />");
                    SmtpClient sC = new SmtpClient("192.9.200.214", 25);
                    mM.Body += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />This mail has been triggered to draw your attention on the respective ERP/Joomla module. Please login to respective module to see further details and file attachments";
                    //   sC.Host = "192.9.200.214"; //"smtp-mail.outlook.com"// smtp.gmail.com
                    //   sC.Port = 25; //587
                    sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sC.UseDefaultCredentials = false;
                    sC.Credentials = new NetworkCredential("baansupport@isgec.co.in", "isgec");
                    //sC.Credentials = new NetworkCredential("adskvaultadmin", "isgec@123");
                    sC.EnableSsl = false;  // true
                    sC.Timeout = 10000000;
                    sC.Send(mM);
                    //  ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Mail has been sent');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please provide proper mail id and Content');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
        }

        #endregion

        #region Attachment
        private void BindData()
        {
            try
            {
                string NotesId = string.Empty;
                objWorkFlow = new WorkFlow();
                objWorkFlow.IndexValue = hdfWFID.Value;
                DataTable dtNotedID = objWorkFlow.GetNoteID();
                foreach (DataRow dr in dtNotedID.Rows)
                {
                    if (NotesId == string.Empty)
                    {
                        NotesId = "'" + dr["NotesId"].ToString() + "'";
                    }
                    else
                    {
                        NotesId += ",'" + dr["NotesId"].ToString() + "'";
                    }
                }
                DataTable dt = new DataTable();
                if (Request.QueryString["user"] != null)
                {
                    // if (HttpContext.Current.Cache["ATHData"] == null)
                    // {
                    objWorkFlow = new WorkFlow();
                    objWorkFlow.AttachmentHandle = "'J_PREORDER_WORKFLOW','JOOMLA_NOTES'";

                    if (NotesId != string.Empty)
                    {
                        objWorkFlow.IndexValue = NotesId + ",'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    }
                    else if (hdfPWFID.Value != "")
                    {
                        objWorkFlow.IndexValue = "'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    }
                    else
                    {
                        objWorkFlow.IndexValue = "'" + hdfWFID.Value + "'";
                    }
                    dt = objWorkFlow.GetAttachments();
                    // HttpContext.Current.Cache.Insert("ATHData", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    //  }
                    //  else
                    //  {
                    ///      dt = (DataTable)HttpContext.Current.Cache["ATHData"];
                    //  }
                    if (dt.Rows.Count > 0)
                    {
                        gvAttachment.DataSource = dt;
                        gvAttachment.DataBind();
                        divNoRecord.Visible = false;
                        divViewAttachment.Visible = true;
                    }
                    else
                    {
                        divNoRecord.Visible = true;
                        divViewAttachment.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No Data found');", true);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }

        protected void gvAttachment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAttachment.PageIndex = e.NewPageIndex;
            BindData();

        }
        protected void UploadAttachment(string NotesId)
        {
            try
            {
                if (Request.QueryString["user"] != null)
                {
                    objWorkFlow = new WorkFlow();
                    // objWorkFlow.IndexValue = Request.QueryString["Index"];
                    objWorkFlow.AttachmentHandle = "JOOMLA_NOTES";
                    DataTable dt = objWorkFlow.GetPath();
                    if (dt.Rows.Count > 0)
                    {
                        //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                        string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                        string LocalPath = Server.MapPath("~/Files/");
                        if (FileUpload.HasFile)
                        {
                            int filecount = 0;
                            filecount = FileUpload.PostedFiles.Count();
                            if (filecount > 0)
                            {
                                foreach (HttpPostedFile PostedFile in FileUpload.PostedFiles)
                                {
                                    string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
                                    string fileExtension = Path.GetExtension(PostedFile.FileName);
                                    try
                                    {
                                        WorkFlow objWorkFlow = new WorkFlow();
                                        objWorkFlow.AttachmentHandle = "JOOMLA_NOTES";
                                        objWorkFlow.IndexValue = NotesId;
                                        objWorkFlow.PurposeCode = "Dmisg134_1_vendor";// Request.QueryString["PurposeCode"];
                                        objWorkFlow.AttachedBy = Request.QueryString["user"];
                                        objWorkFlow.FileName = fileName + fileExtension;
                                        objWorkFlow.LibraryCode = dt.Rows[0]["LibCode"].ToString();
                                        // DataTable dtFile = objWorkFlow.GetFileName();
                                        //  if (dtFile.Rows.Count == 0)
                                        //  {
                                        DataTable dtDocID = objWorkFlow.InsertAttachment();
                                        if (dtDocID.Rows[0][0].ToString() != "0")
                                        {
                                            try
                                            {
                                                FileUpload.SaveAs(ServerPath + dtDocID.Rows[0][0]);
                                            }
                                            catch (Exception ex)
                                            {// err.Text = ex.Message; 
                                            }
                                            // FileUpload.SaveAs(LocalPath + fileName + fileExtension);
                                            HttpContext.Current.Cache.Remove("ATHData");
                                            BindData();
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
                                        }
                                        else
                                        {
                                            //objWorkFlow = new AttachmentCls();
                                            //objWorkFlow.DocumentId = dtDocID.Rows[0][0].ToString();
                                            //int res = objWorkFlow.DeleteAttachment();
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
                                        }
                                        //  }
                                        //  else
                                        //  {
                                        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('This file name already exist please change your file name');", true);
                                        // }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found Properly');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string[] values = lnkbtn.CommandArgument.Split('@');
                WorkFlow objWorkFlow = new WorkFlow();
                objWorkFlow.AttachmentHandle = "JOOMLA_NOTES";
                DataTable dt = objWorkFlow.GetPath();
                //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\" + values[0];// dt.Rows[0]["Path"].ToString() + "\\" + values[0];// Server.MapPath("~/Files/") + values[0]; // 
                string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\" + values[0];
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + values[1] + "\"");
                byte[] data = req.DownloadData(ServerPath);
                response.BinaryWrite(data);
                response.End();
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Could not find');", true);
            }
        }
        #endregion
    }
}