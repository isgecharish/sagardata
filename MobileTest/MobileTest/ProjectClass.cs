using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MobileTest
{
    public class ProjectClass
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string ConERPLive= ConfigurationManager.AppSettings["ConnectionERPLive"];
        //ConnectionERPLive
        public string IndexValue { get; set; }
        public string Remarks { get; set; }
        public string DBID { get; set; }
        public string TableName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string NoteID { get; set; }
        public string NotesHandle { get; set; }
        //AttachmentHandle
        public string AttachmentHandle { get; set; }
        public string TableDesc { get; set; }
        public string RemiderMailId { get; set; }
        public string ReminderDateTime { get; set; }
        public string Color { get; set; }
        public string SendEmailTo { get; set; }
        public string DocumentId { get; set; }
        public string PurposeCode { get; set; }
        public string FileName { get; set; }
        public string LibraryCode { get; set; }
        public string AttachedBy { get; set; }
        public string LibraryDescription { get; set; }
        public string ServerName { get; set; }
        public string Path { get; set; }
        public string IsActive { get; set; }
        public string DatabaseName { get; set; }
        public string DBServer { get; set; }
        public string DBDescription { get; set; }
        public string PurposeDesc { get; set; }
        public string RandomNo { get; set; }
        public string Activity { get; set; }
        //private DateTime scdt;
        public DateTime ScheduledFinishedDate { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualFinishDate { get; set; }
        public string sRemarks { get; set; }
        public string sProjName { get; set; }
        public string ActivityID { get; set; }
        public string sProjId { get; set; }

        public string sDrpProj { get; set;}

        public DataTable GetProjectProgressByID(string username,string ProjectId,string Activity)
        {
            string sGetProjectProgress = @"select t_cprj,t_cact,t_sdst,t_sdfn,t_acsd,t_acfn, t_dsca,t_remk,t_otst,t_otfn from ttpisg910200 WHERE t_cprj = '" + ProjectId + "' and t_cact='" + Activity + "'";
            if (username == "isgec" || username == "3194" || username == "0330")

            { return SqlHelper.ExecuteDataset(Con, CommandType.Text, sGetProjectProgress).Tables[0]; }
            else
            { 
            return SqlHelper.ExecuteDataset(ConLive, CommandType.Text, sGetProjectProgress).Tables[0];
            }
        }
        public int UpdatRecords(string username,string ProjectId, string Activity,string Actual_SDate, string Actual_FDate, string Remarks,string Outlook_SDate, string Outlook_FDate)
        {
            string sUpdateRecords = @"Update ttpisg910200 set t_acsd ='" + Actual_SDate + "' ,t_remk ='" + Remarks + "', t_acfn ='" + Actual_FDate + @"'
                           , t_otst ='" + Outlook_SDate + "', t_otfn ='" + Outlook_FDate + "' WHERE t_cprj = '" + ProjectId + "' and t_cact ='" + Activity + "'";

            if (username == "isgec" || username == "3194" || username == "0330")
            {
                return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, sUpdateRecords);
            }
            else
            { 
            return SqlHelper.ExecuteNonQuery(ConLive, CommandType.Text, sUpdateRecords);
            }
        }

        public int ValidateUser(string Username, string Password)
        {
            string sUserRecord = @"select count(*) from ttpisg228200 where t_user= '" + Username + "' and t_pswd='" + Password + "'";
            if (Username == "isgec" || Username == "3194" || Username == "0330")
            {
                return (int) (SqlHelper.ExecuteScalar(Con, CommandType.Text, sUserRecord));
            }
            else
            {
                return (int)(SqlHelper.ExecuteScalar(ConLive, CommandType.Text, sUserRecord));
            }
        }
        public DataTable GetUserRecord(string LoginId)
        {
            string sUserDetails = @"select  (a.t_cprj + '-' + b.t_dsca) AS drpdwn, a.t_cprj, a.t_day, b.t_dsca, a.t_dept from ttpisg225200 a inner join ttcmcs052200 b on a.t_cprj=b.t_cprj where t_logn= '" + LoginId + "'";
            if (LoginId == "isgec" || LoginId == "3194" || LoginId == "0330")
            {
                return SqlHelper.ExecuteDataset(Con, CommandType.Text, sUserDetails).Tables[0];
            }
            else
            { 
            return SqlHelper.ExecuteDataset(ConLive, CommandType.Text, sUserDetails).Tables[0];
            }
        }

        public DataTable GetSelectedProjectDetails()
        {
            string sProjRecord = @"select a.t_day, b.t_dsca, a.t_dept from ttpisg225200 a inner join ttcmcs052200 b on a.t_cprj=b.t_cprj where a.t_cprj = '" + sProjId + "' and a.t_logn = '" + User + "'";
            if (User == "isgec" || User == "3194" || User == "0330")

            {
               return (SqlHelper.ExecuteDataset(Con, CommandType.Text, sProjRecord)).Tables[0];
            }
            else
            {
                return (SqlHelper.ExecuteDataset(ConLive, CommandType.Text, sProjRecord)).Tables[0];
            }
        }

        public DataTable GetALLHandleByID()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, "select NotesHandle,AccessIndex,DBID,TableName,TableDescription,Remarks from Note_Handle Where NotesHandle='" + NotesHandle + "'").Tables[0];
        }
        public DataTable GetNotes()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"Select Notes_RunningNo,NotesId,NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.EmployeeName,NUC.ColorId,NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN HRM_Employees EMP ON EMP.CardNo=NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                        Where NN.NotesHandle='" + NotesHandle + "' and NN.IndexValue='" + IndexValue + "' order by Notes_RunningNo").Tables[0];
        }

        public DataTable GetNoteID()
        {
            string sNotes = @"select IndexValue from Note_Notes where NotesId='" + IndexValue + "'";
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, sNotes).Tables[0]; //
        }
        public DataTable GetNotesFromASPNETUSer()
        {
            string sGetNotes = @"Select Notes_RunningNo, NotesId, NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.UserFullName as EmployeeName,NUC.ColorId,
                        NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN aspnet_users EMP ON EMP.LoginId= NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId= NN.UserId
                        Where NN.NotesHandle='" + NotesHandle + "'  order by Notes_RunningNo";
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, sGetNotes).Tables[0]; //and NN.IndexValue='" + IndexValue + "'
        }
        public DataTable GetAllNotes()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"Select Notes_RunningNo,NotesId,NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.EmployeeName,NUC.ColorId,NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN HRM_Employees EMP ON EMP.CardNo=NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                        order by Notes_RunningNo").Tables[0];
        }
        public DataTable Insertdata()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@NotesHandle", NotesHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@Title", Title));
            sqlParamater.Add(new SqlParameter("@Description", Description));
            sqlParamater.Add(new SqlParameter("@User", @User));
            sqlParamater.Add(new SqlParameter("@ReminderEmailId", RemiderMailId));
            sqlParamater.Add(new SqlParameter("@ReminderDateTime", ReminderDateTime));
            sqlParamater.Add(new SqlParameter("@SendEmailTo", SendEmailTo));
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.StoredProcedure, "[sp_InsertNotes]", sqlParamater.ToArray()).Tables[0];
           // return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Insert Query").Tables[0];
        }
        public string GetTempNoteID()
        {
            DataTable dt = SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"SELECT {fn CONCAT(CAST(Series as varchar),CAST(ISNULL(MAX(RunningNo),0) + 1  as varchar))}
                        as DocumentID FROM Note_DocumentSeries where Active='Y'
                        GROUP By Series").Tables[0];

            string TempNoteId = dt.Rows[0][0].ToString();
            return TempNoteId;


        }
        public string GetLogisticCompany(string projId)
        {
            string sLogfromProj= @"select LogisticCompany from LN_ProjectMaster where ProjectCode = '" + projId + "'";
            string sLogComany=  SqlHelper.ExecuteScalar(ConLive, CommandType.Text, sLogfromProj).ToString();
                return sLogComany;
        }
        public DataTable GetPath()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            //  sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_GetAttachmentPath", sqlParamater.ToArray()).Tables[0];
        }

        public DataTable GetAttachments()
        {
            string sGetAttachmnt = @"
                                    SELECT [t_drid]
                                          ,[t_dcid]
                                          ,[t_hndl]
                                          ,[t_indx]
                                          ,[t_fnam]
                                          ,ATH.[t_prcd]
                                          ,[t_lbcd]
                                          ,[t_atby]
                                          ,[t_aton]
                                          ,t_desc
                                      FROM [ttcisg132200]  ATH
                                      LEFT JOIN  ttcisg129200 AP ON AP.t_prcd=ATH.t_prcd
                                      WHERE t_hndl = '" + AttachmentHandle + "' and t_indx in('" + IndexValue + "')";
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, sGetAttachmnt).Tables[0]; //
        }
        public DataTable GetAllAttachments()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"
                                    SELECT [t_drid]
                                          ,[t_dcid]
                                          ,[t_hndl]
                                          ,[t_indx]
                                          ,[t_fnam]
                                          ,ATH.[t_prcd]
                                          ,[t_lbcd]
                                          ,[t_atby]
                                          ,[t_aton]
                                          ,t_desc
                                      FROM [ttcisg132200] ATH
                                      LEFT JOIN  ttcisg129200 AP ON AP.t_prcd=ATH.t_prcd Where ATH.t_hndl='T_ERECTIONACTIVITY'" ).Tables[0]; //
        }
        public DataTable GetEmployeeDetails()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"select EmployeeName, EmailID from HRM_Employees
                                        where CardNo ='" + User + "'").Tables[0];
        }
        public DataTable InsertAttachment()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@PurposeCode", PurposeCode));
            sqlParamater.Add(new SqlParameter("@FileName", FileName));
            sqlParamater.Add(new SqlParameter("@LibraryCode", LibraryCode));
            sqlParamater.Add(new SqlParameter("@AttachedBy", AttachedBy));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_InsertAttachment", sqlParamater.ToArray()).Tables[0];
        }
        public int DeleteNotes()
        {
            return SqlHelper.ExecuteNonQuery(ConERPLive, CommandType.Text, @"Delete from [Note_Notes]
                     WHERE  NotesId= '" + NoteID + @"'");
        }
        public DataTable GetNotesByRunningId()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"Select Notes_RunningNo,NN.NotesId,NotesHandle,IndexValue,Title,Description,
                     NN.UserId, Created_Date, NR.ReminderTo,NR.ReminderDateTime,NUC.ColorId,NN.SendEmailTo  from Note_Notes NN 
                     LEFT JOIN Note_Reminder NR ON NR.NotesId = NN.NotesId
                     LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                     Where NN.NotesId  ='" + NoteID + "'").Tables[0];
        }
        public DataTable GetWFById()
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, @"select WF.[WFID]
                                      ,WF.[Parent_WFID]
                                      ,WF.[Project]
                                      ,WF.[Element]
                                      ,WF.[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=WF.Buyer) as BuyerName
                                      ,WF.[Buyer]
                                      ,WF.[WF_Status]
                                      ,WF.[UserId]
                                      ,WF.[DateTime]
                                      ,WF.[Supplier]
                                      ,WF.[SupplierName]
                                      ,EmployeeName,RandomNo,Notes
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                INNER JOIN [WF1_PreOrder_History] WFH ON WFH.WFID=WF.WFID").Tables[0]; //
        }

        public DataTable GetMAilID(string UserID)
        {
            return SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, "select CardNo,EmployeeName,EMailid  from HRM_Employees where CardNo= '" + UserID + "'").Tables[0];
        }

        public int UpdateNotes()
        {
            if (RemiderMailId != "" && ReminderDateTime != "")
            {
                DataTable dtNoteID = SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, "select NotesID from Note_Reminder where NotesId='" + NoteID + "'").Tables[0];
                if (dtNoteID.Rows.Count == 0)
                {
                    DataTable dtReminderId = SqlHelper.ExecuteDataset(ConERPLive, CommandType.Text, "SELECT(ISNULL(MAX(ReminderId), 0) + 1) as ReminderId FROM Note_Reminder").Tables[0];
                    SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"
                        INSERT INTO[Note_Reminder]
                       ([ReminderID]
                       ,[NotesId]
                       ,[ReminderTo]
                       ,[ReminderDateTime]
                       ,[Status]
                       ,[User]
                       ,[SendEmailTo]
                       )
                 VALUES('" + dtReminderId.Rows[0][0] + @"'
                       , '" + NoteID + @"'
                       , '" + RemiderMailId + @"'
                       , '" + Convert.ToDateTime(ReminderDateTime).ToString("yyyy-MM-dd HH:mm") + @"'
                       ,'New'
                       , '" + User + @"'
                       , '" + SendEmailTo + @"'
                       )");
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(ConERPLive, CommandType.Text, "update Note_Reminder set ReminderTo='" + RemiderMailId + "', ReminderDateTime='" + Convert.ToDateTime(ReminderDateTime).ToString("yyyy-MM-dd HH:mm") + "',Status='New' WHERE NotesId='" + NoteID + "'");
                }
            }
            else
            {

            }
            return SqlHelper.ExecuteNonQuery(ConERPLive, CommandType.Text, @"UPDATE [Note_Notes]
                       SET [Title] = '" + Title + @"'
                          ,[Description] = '" + Description + @"'
                     WHERE NotesId= '" + NoteID + @"'");
        }


    }
}