using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace ProjectProgressUpdateTest
{
    public partial class UpdateProjectProgress : System.Web.UI.Page
    {
        ProjectClass objProjectCls;
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
            try
            {
                if (!IsPostBack)
                {
                    string sProjId = (string)(Session["projId"]);
                    string sProjName = (string)(Session["projName"]);
                    string sUsername = (string)(Session["Username"]);
                    lblProjDetail1.Text = sProjId + " - " + sProjName;
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    GetNotes();
                }
               

            }
            catch (Exception ex)
                {
                    string tt = ex.Message;
                }
           

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
           string sUsername = (string)(Session["Username"]);
            if (Request.QueryString["cprj"] != null && Request.QueryString["cact"] != null)
            {
                GetProjectProgress(sUsername);
            }
        }
        protected void GetProjectProgress(string username)
        {
            objProjectCls = new ProjectClass();
            DataTable dt = objProjectCls.GetProjectProgressByID(username,Request.QueryString["cprj"],Request.QueryString["cact"]);
            if (dt.Rows.Count > 0)
            {
                txtActivity.Text = dt.Rows[0]["t_cact"].ToString();
                txtDescription.Text = dt.Rows[0]["t_dsca"].ToString();

                txtScheduledStartDate.Text = Convert.ToDateTime(dt.Rows[0]["t_sdst"]).ToString("dd-MM-yyyy");
                txtScheduledFinishDate.Text = Convert.ToDateTime(dt.Rows[0]["t_sdfn"]).ToString("dd-MM-yyyy");
                txtRemarks.Text = dt.Rows[0]["t_remk"].ToString();
            }
            txtActualStartDate.Attributes.Add("type", "date");
            if (( Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy") == "1753"|| Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy") == "1900"|| (dt.Rows[0]["t_acsd"]).ToString() == string.Empty))
            {
                string sdate = "yyyy-MM-dd";
                txtActualStartDate.Attributes.Add("value", sdate);

            }
            else
            {
                txtActualStartDate.Attributes.Add("value", Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy-MM-dd"));
            }
            txtActualFinishDate.Attributes.Add("type", "date");
            if (( Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy") == "1753"|| Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy") == "1900" || (dt.Rows[0]["t_acfn"]).ToString() == string.Empty))
            {
                string fdate = "yyyy-MM-dd";
                txtActualFinishDate.Attributes.Add("value", fdate);

            }
            else
            {
                txtActualFinishDate.Attributes.Add("value", Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy-MM-dd"));
                btnUpdate.Enabled = false;
            }
          
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sUsername = (string)(Session["Username"]);
            string Actual_SDate;
            string Actual_FDate;
            string Remarks;
            try
            {
                objProjectCls = new ProjectClass();
                if (txtActualStartDate.Text == "")
                    Actual_SDate = "";
                else { Actual_SDate= Convert.ToDateTime(txtActualStartDate.Text).ToString("yyyy-MM-dd"); }
                if (txtActualFinishDate.Text == "")
                    Actual_FDate = "";
                else { Actual_FDate = Convert.ToDateTime(txtActualFinishDate.Text).ToString("yyyy-MM-dd"); }
                if (txtRemarks.Text == "")
                    Remarks = "";
                else { Remarks = txtRemarks.Text.ToString(); }
                int res = objProjectCls.UpdatRecords(sUsername,Request.QueryString["cprj"], Request.QueryString["cact"], Actual_SDate, Actual_FDate,Remarks);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                }
            }

            catch (Exception ex)
            {
                string tt = ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectProgressReport.aspx");
        }

        private void GetData()
        {
            try
            {
                objProjectCls = new ProjectClass();
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194"; // for testing only 
                 // objProjectCls.RandomNo = Request.QueryString["user"];
                objProjectCls.RandomNo = sUsername;
                DataTable dt = objProjectCls.GetWFById();

                if (dt.Rows.Count > 0)
                {
                    //txtProject.Text = dt.Rows[0]["Project"].ToString();
                    //txtElement.Text = dt.Rows[0]["Element"].ToString();
                    //txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                    //txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                    //txtSupplierEmail.Text = dt.Rows[0]["Supplier"].ToString();
                    //txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
                    //txtNotes.Text = dt.Rows[0]["Notes"].ToString();
                    hdfWFID.Value = dt.Rows[0]["WFID"].ToString();
                    hdfPWFID.Value = dt.Rows[0]["Parent_WFID"].ToString();
                    //---
                    DataTable dtMailTo = objProjectCls.GetMAilID(dt.Rows[0]["Buyer"].ToString());
                    string BuyrMail = dtMailTo.Rows[0]["EMailid"].ToString();
                    txtMailTo.Text = BuyrMail;

                    //----
                    string Header = string.Empty;
                    //if (txtSpecification.Text.Contains("("))
                    //{
                    //    string[] Heading = txtSpecification.Text.Split('(');
                    //    Header = Heading[0];
                    //}
                    //else
                    //{
                    //    Header = txtSpecification.Text;
                    //}
                    ////---
                    //string Title = txtSpecification.Text;
                    txtTitle.Text = Title;

                   // spIndex.InnerHtml = Header;


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
                objProjectCls = new ProjectClass();
                objProjectCls.NotesHandle = "T_ERECTIONACTIVITY";
               // objProjectCls.IndexValue = hdfWFID.Value;
                DataTable dt = objProjectCls.GetNotesFromASPNETUSer();
                if (dt.Rows.Count > 0)
                {
                    rptNotes.DataSource = dt;
                    rptNotes.DataBind();
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No record found');", true);
                }
                DataTable dtAttach = objProjectCls.GetAllAttachments();
                if (dtAttach.Rows.Count > 0)
                {
                    gvAttachment.DataSource = dtAttach;
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
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194"; // for testing only 
                if (txtTitle.Text != "" && txtNotesDescription.Text != "")
                {
                    objProjectCls = new ProjectClass();
                    objProjectCls.NotesHandle = "T_ERECTIONACTIVITY";
                    objProjectCls.IndexValue = hdfWFID.Value;
                    objProjectCls.Title = txtTitle.Text.Trim();
                    objProjectCls.Description = txtNotesDescription.Text.Trim();
                    //objProjectCls.User = Request.QueryString["user"];
                    objProjectCls.User = sUsername;
                    objProjectCls.SendEmailTo = txtMailTo.Text;
                    objProjectCls.RemiderMailId = "";
                    objProjectCls.ReminderDateTime = "";
                    //objNotes.ReminderDateTime =txtDate.Text!=""? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") +" "+ txtTime.Text.Trim(): System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + txtTime.Text.Trim();

                    if (btnSaveNotes.Text == "Submit")
                    {
                        //if (hdfNewNoteId.Value == "")
                        //{
                        DataTable dtNotesID = objProjectCls.Insertdata();
                        if (dtNotesID.Rows[0][0].ToString() != "0")
                        {
                            if (txtMailTo.Text != "")
                            {
                                SendMAil();
                            }
                            UploadAttachment(dtNotesID.Rows[0][0].ToString());
                            txtTitle.Text = "";
                            txtNotesDescription.Text = "";
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
                        objProjectCls.NoteID = hdfNoteId.Value;
                        int res = objProjectCls.UpdateNotes();
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
                objProjectCls = new ProjectClass();
                objProjectCls.NoteID = Value[1];
                DataTable dt = objProjectCls.GetNotesByRunningId();
                txtMailTo.Text = dt.Rows[0]["SendEmailTo"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtNotesDescription.Text = dt.Rows[0]["Description"].ToString();
                //add color to desc and button
                txtNotesDescription.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                btnNewNotes.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                //------
                //   txtMailIdReminder.Text = dt.Rows[0]["ReminderTo"].ToString();
                //  txtDate.Text = dt.Rows[0]["ReminderDateTime"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["ReminderDateTime"].ToString()).ToString("dd-MM-yyyy") : "";
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194";// for testing only
                //if (uId == Request.QueryString["user"])
                if (uId == sUsername)
                {
                    txtTitle.Enabled = true;
                    txtNotesDescription.Enabled = true;
                    btnSaveNotes.Text = "Update";
                    btnSaveNotes.Enabled = true;
                    btnDeleteNotes.Enabled = true;
                    btnDeleteNotes.Visible = true;
                    txtMailTo.Enabled = true;
                }
                else
                {
                    txtTitle.Enabled = false;
                    txtNotesDescription.Enabled = false;
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
            txtNotesDescription.Enabled = true;
            txtTitle.Text = "";
            txtNotesDescription.Text = "";
            btnSaveNotes.Text = "Submit";
            btnDeleteNotes.Visible = false;
            btnSaveNotes.Enabled = true;
            txtMailTo.Text = Request.QueryString["Em"];
            txtTitle.Text = Request.QueryString["Tl"];
            //spIndex.InnerHtml = Request.QueryString["Hd"];
            hdfNoteId.Value = "";
            hdfNewNoteId.Value = "";
        }
        protected void btnDeleteNotes_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectCls = new ProjectClass();
                objProjectCls.NoteID = hdfNoteId.Value;
                int res = objProjectCls.DeleteNotes();
                if (res > 0)
                {
                    txtTitle.Text = "";
                    txtNotesDescription.Text = "";
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
                    //  mM.From = new MailAddress(txtSupplierEmail.Text);
                    mM.From = new MailAddress("sagar.shukla@isgec.co.in"); // for testing only
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
                    mM.Subject = txtTitle.Text.Trim();
                  //  mM.Subject = txtTitle.Text.Trim() + "-" + spIndex.InnerHtml;
                    // string file = Server.MapPath("~/Files/") + hdfFile.Value;
                    // mM.Attachments.Add(new System.Net.Mail.Attachment(file));
                    mM.Body = txtNotesDescription.Text.Trim();
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
                objProjectCls = new ProjectClass();
                objProjectCls.IndexValue = hdfWFID.Value;
                DataTable dtNotedID = objProjectCls.GetNoteID();
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
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194";// for testing only
                //if (Request.QueryString["user"] != null)
                if (sUsername != null)
                {
                    // if (HttpContext.Current.Cache["ATHData"] == null)
                    // {
                    objProjectCls = new ProjectClass();
                    objProjectCls.AttachmentHandle = "T_ERECTIONACTIVITY";

                    if (NotesId != string.Empty)
                    {
                        objProjectCls.IndexValue = NotesId + ",'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    }
                    else if (hdfPWFID.Value != "")
                    {
                        objProjectCls.IndexValue = "'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    }
                    else
                    {
                        objProjectCls.IndexValue = "'" + hdfWFID.Value + "'";
                    }
                    dt = objProjectCls.GetAttachments();
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
        private void AttachmentBindData(string sNotesId)
        {
            try
            {
               // string NotesId = string.Empty;
                //objProjectCls = new ProjectClass();
                //objProjectCls.IndexValue = sNotesId;
                //DataTable dtNotedID = objProjectCls.GetNoteID();
                //foreach (DataRow dr in dtNotedID.Rows)
                //{
                //    if (NotesId == string.Empty)
                //    {
                //        NotesId = "'" + dr["NotesId"].ToString() + "'";
                //    }
                //    else
                //    {
                //        NotesId += ",'" + dr["NotesId"].ToString() + "'";
                //    }
                //}
                DataTable dt = new DataTable();
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194";// for testing only
                                   ////if (Request.QueryString["user"] != null)
                if (sUsername != null)
                {
                    //    // if (HttpContext.Current.Cache["ATHData"] == null)
                    //    // {
                    objProjectCls = new ProjectClass();
                    objProjectCls.IndexValue = sNotesId;
                    objProjectCls.AttachmentHandle = "T_ERECTIONACTIVITY";

                    //    if (NotesId != string.Empty)
                    //    {
                    //        objProjectCls.IndexValue = NotesId + ",'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    //    }
                    //    else if (hdfPWFID.Value != "")
                    //    {
                    //        objProjectCls.IndexValue = "'" + hdfPWFID.Value + "'" + ",'" + hdfWFID.Value + "'";
                    //    }
                    //    else
                    //    {
                    //        objProjectCls.IndexValue = "'" + hdfWFID.Value + "'";
                    //    }
                    dt = objProjectCls.GetAttachments();
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
                string sUsername = (string)(Session["Username"]);
                sUsername = "3194";// for testing only
                 //if (Request.QueryString["user"] != null)
                if (sUsername != null)
                {
                    objProjectCls = new ProjectClass();
                    // objProjectCls.IndexValue = Request.QueryString["Index"];
                    objProjectCls.AttachmentHandle = "T_ERECTIONACTIVITY";
                    DataTable dt = objProjectCls.GetPath();
                    if (dt.Rows.Count > 0)
                    {
                        //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                        string ServerPath = "E:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                                                                                              //attachmentlibrary1
                                                                                              //  string ServerPath = "E:\\attachmentlibrary1\\";
                        string LocalPath = Server.MapPath("~/Files/");
                        if (FileUpload.HasFile)
                        {
                            int filecount = 0;
                            filecount = FileUpload.PostedFiles.Count;
                            if (filecount > 0)
                            {
                                foreach (HttpPostedFile PostedFile in FileUpload.PostedFiles)
                                {
                                    string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
                                    string fileExtension = Path.GetExtension(PostedFile.FileName);
                                    try
                                    {
                                        objProjectCls = new ProjectClass();
                                        objProjectCls.AttachmentHandle = "T_ERECTIONACTIVITY";
                                        objProjectCls.IndexValue = NotesId;
                                        objProjectCls.PurposeCode = "Attachment for Mobile App Notes";// Request.QueryString["PurposeCode"];

                                        //objProjectCls.AttachedBy = Request.QueryString["user"];
                                        objProjectCls.AttachedBy = sUsername;
                                        objProjectCls.FileName = fileName + fileExtension;
                                        objProjectCls.LibraryCode = "LIB000001";
                                        //dt.Rows[0]["LibCode"].ToString();
                                        // DataTable dtFile = objProjectCls.GetFileName();
                                        //  if (dtFile.Rows.Count == 0)
                                        //  {
                                        DataTable dtDocID = objProjectCls.InsertAttachment();
                                        if (dtDocID.Rows[0][0].ToString() != "0")
                                        {
                                            try
                                            {
                                                string AttachServerPath = ServerPath + dtDocID.Rows[0][0];
                                                FileUpload.SaveAs(AttachServerPath);
                                                //  PostFile.SaveAs(AttachServerPath);


                                            }
                                            catch (Exception ex)
                                            {// err.Text = ex.Message; 
                                            }
                                            // FileUpload.SaveAs(LocalPath + fileName + fileExtension);
                                            HttpContext.Current.Cache.Remove("ATHData");
                                            AttachmentBindData(NotesId);
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
                                        }
                                        else
                                        {
                                            //objProjectCls = new AttachmentCls();
                                            //objProjectCls.DocumentId = dtDocID.Rows[0][0].ToString();
                                            //int res = objProjectCls.DeleteAttachment();
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
                    }
                    else
                    {

                    }
                   // }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
                    //}
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
                 objProjectCls = new ProjectClass();
                objProjectCls.AttachmentHandle = "T_ERECTIONACTIVITY";
                DataTable dt = objProjectCls.GetPath();
                //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\" + values[0];// dt.Rows[0]["Path"].ToString() + "\\" + values[0];// Server.MapPath("~/Files/") + values[0]; // 
                string ServerPath = "E:\\attachmentlibrary1\\" + values[0];
                //"D:\\" + dt.Rows[0]["t_path"].ToString() + "\\" + values[0];
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