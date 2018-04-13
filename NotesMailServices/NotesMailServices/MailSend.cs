using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace NotesMailServices
{
    public class MailSend
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];
        public DataTable GetReminderTime()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select ReminderId,NotesId,ReminderTo,ReminderDateTime,Status,EmployeeName,EMailID from Note_Reminder NR
                            INNER JOIN HRM_Employees E ON E.CardNo = NR.[User] WHERE Status='New' and  ReminderId!='1'").Tables[0]; //
        }
        public DataTable GetNotes(string NoteId)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select IndexValue, Title,Description,TableDescription From Note_Notes NN
                                  INNER JOIN ATH_Handle NH ON NH.Attachment_Handle = NN.NotesHandle
                                  Where NotesId = '" + NoteId + "'").Tables[0];
        }
        public void SendMail(string SendMailId, string UserMailId, string Title, string Description, int ReminderId)
        {
            try
            {
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress(UserMailId);
                string[] MailTo = SendMailId.Split(';');
                foreach (string Mailid in MailTo)
                {
                    mM.To.Add(new MailAddress(Mailid));
                }
                mM.To.Add(UserMailId);
                mM.Subject = Title;
                // string file = Server.MapPath("~/Files/") + hdfFile.Value;
                // mM.Attachments.Add(new System.Net.Mail.Attachment(file));
                mM.Body = Description;
                mM.IsBodyHtml = true;
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
                // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Mail has been sent');", true);

                UpdateMailStatus(ReminderId);
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateMailStatus(int ReminderId)
        {
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update Note_Reminder SET Status='Sent' where ReminderId='" + ReminderId + "'");
        }
        public DataTable GetTestMail()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select ReminderId,ReminderDateTime,ReminderTo,Status from Note_Reminder where ReminderId='1'").Tables[0];
        }
        public void UpdateTestMailTime(string DateTime)
        {
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update Note_Reminder SET Status='New',ReminderDateTime='" + DateTime + "' where ReminderId='1'");
        }
    }
}
