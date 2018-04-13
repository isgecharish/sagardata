using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotesDairy
{
    public partial class Notes : System.Web.UI.Page
    {
        NotesClass objNotes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetNotes();
            }
        }
        private void GetNotes()
        {
            objNotes = new NotesClass();
            objNotes.NotesHandle = Request.QueryString["handle"];
            objNotes.IndexValue = Request.QueryString["index"];
            DataTable dt = objNotes.GetNotes();
            rptNotes.DataSource = dt;
            rptNotes.DataBind();
        }
        protected void btnSaveNotes_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "" && txtDescription.Text != "")
            {
                objNotes = new NotesClass();
                objNotes.NotesHandle = Request.QueryString["handle"];
                objNotes.IndexValue = Request.QueryString["index"];
                objNotes.Title = txtTitle.Text.Trim();
                objNotes.Description = txtDescription.Text.Trim();
                objNotes.User = Request.QueryString["user"];
                objNotes.NoteID = hdfNoteId.Value;
                if (btnSaveNotes.Text == "Save")
                {
                    DataTable dtNotesID = objNotes.Insertdata();
                    if (dtNotesID.Rows.Count > 0)
                    {
                        GetNotes();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Saved');", true);
                    }
                }
                else
                {
                    int res = objNotes.UpdateNotes();
                    if (res > 0)
                    {
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

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            string[] Value = lnkBtn.CommandArgument.Split('&');
            string uId = Value[0];
            hdfNoteId.Value = Value[1];
            objNotes = new NotesClass();
            objNotes.NoteID = Value[1];
            DataTable dt = objNotes.GetNotesByRunningId();
            txtTitle.Text = dt.Rows[0]["Title"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            if (uId == Request.QueryString["user"])
            {
                txtTitle.Enabled = true;
                txtDescription.Enabled = true;
                btnSaveNotes.Text = "Update";
                btnSaveNotes.Visible = true;
                btnDeleteNotes.Visible = true;
            }
            else
            {
                txtTitle.Enabled = false;
                txtDescription.Enabled = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('You are not authorised to update records');", true);
                btnSaveNotes.Visible = false;
                btnDeleteNotes.Visible = false;
            }
        }

        protected void btnNewNotes_Click(object sender, EventArgs e)
        {
            txtTitle.Enabled = true;
            txtDescription.Enabled = true;
            txtTitle.Text = "";
            txtDescription.Text = "";
            btnSaveNotes.Text = "Save";
            btnSaveNotes.Visible = true;
            btnDeleteNotes.Visible = false;
        }

        protected void btnDeleteNotes_Click(object sender, EventArgs e)
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
    }
}