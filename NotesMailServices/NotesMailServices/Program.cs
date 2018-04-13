using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesMailServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ReminderTo = string.Empty;
            string Title = string.Empty;

            string Description = string.Empty;

            MailSend objMail = new MailSend();
            DataTable dt = objMail.GetReminderTime();
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToDateTime(dr["ReminderDateTime"]).ToString("yyyy-MM-dd") == System.DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    DataTable dtNotes = objMail.GetNotes(dr["NotesId"].ToString());
                    if (dtNotes.Rows.Count > 0)
                    {
                        string Subject = "Alert - " + dtNotes.Rows[0]["Title"].ToString() + "-" + dtNotes.Rows[0]["TableDescription"].ToString() + "," + dtNotes.Rows[0]["IndexValue"].ToString();
                        objMail.SendMail(dr["ReminderTo"].ToString(), dr["EMailID"].ToString(), Subject, dtNotes.Rows[0]["Description"].ToString(), Convert.ToInt32(dr["ReminderId"]));

                        if (ReminderTo == string.Empty)
                        {
                            ReminderTo = dr["ReminderTo"].ToString();
                        }
                        else
                        {
                            ReminderTo += "<br/>";
                            ReminderTo += dr["ReminderTo"].ToString();
                        }
                        //Title
                        if (Title == string.Empty)
                        {
                            Title = dtNotes.Rows[0]["Title"].ToString();
                        }
                        else
                        {
                            Title += "<br/>";
                            Title += dtNotes.Rows[0]["Title"].ToString();
                        }
                        Description = "<table border='1' style='border-collapse:collapse;'><tr><th style='border:1px solid black'>Mail To</th><th style='border:1px solid black'>Title</th></tr><tr><td>" + ReminderTo + "</td><td>" + Title + "</td></tr></table>";
                    }
                }
            }

            // Da
            DataTable dtTest = objMail.GetTestMail();
            if (Convert.ToDateTime(dtTest.Rows[0]["ReminderDateTime"]).ToString("yyyy-MM-dd") == System.DateTime.Now.ToString("yyyy-MM-dd"))
            {
                objMail.SendMail(dtTest.Rows[0]["ReminderTo"].ToString(), "veena.pawar@isgec.co.in", "Today's Notes Reminder Details", Description!=""?Description:"No Reminder", Convert.ToInt32(dtTest.Rows[0]["ReminderId"]));
                if (System.DateTime.Now.DayOfWeek.ToString() == "Saturday")
                {
                    string DateTime = System.DateTime.Now.AddDays(2).ToString("yyyy-MM-dd 9:00:00");
                    objMail.UpdateTestMailTime(DateTime);
                }
                else
                {
                    string DateTime = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 9:00:00");
                    objMail.UpdateTestMailTime(DateTime);
                }
            }
        }
    }
}
