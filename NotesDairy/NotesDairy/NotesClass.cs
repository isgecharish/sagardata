using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NotesDairy
{
    public class NotesClass
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];
        public string NotesHandle { get; set; }
        public string IndexValue { get; set; }
        public string Remarks { get; set; }
        public string DBID { get; set; }
        public string TableName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string NoteID { get; set; }
        public DataTable GetALLNotesHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select NotesHandle,AccessIndex,DBID,TableName,Remarks from Note_Handle").Tables[0];
        }
        public DataTable GetALLDBID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select DBID,DbDescription,DbServerName,DatabaseName,libraryCode FROM ATH_Database").Tables[0];
        }
        public DataTable GetALLHandleByID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select NotesHandle,AccessIndex,DBID,TableName,Remarks from Note_Handle Where NotesHandle='" + NotesHandle + "'").Tables[0];
        }
        public DataTable GetUsedHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select * from Note_Notes Where NotesHandle='" + NotesHandle + "'").Tables[0];
        }

        public DataTable GetNotesByRunningId()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select * from Note_Notes Where NotesId='" + NoteID + "'").Tables[0];
        }
        public DataTable GetNotes()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select * from Note_Notes Where NotesHandle='" + NotesHandle + "' and IndexValue='"+IndexValue+"'").Tables[0];
        }
        public int InsertNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [Note_Handle]
           ([NotesHandle]
           ,[DBID]
           ,[TableName]
           ,[AccessIndex]
           ,[Remarks])
     VALUES
           ('" + NotesHandle + "','" + DBID + "','" + TableName + "','" + IndexValue + "','" + Remarks + "')");
        }
        public int UpdateNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [Note_Handle]
                       SET [DBID] = '" + DBID + @"'
                          ,[TableName] = '" + TableName + @"'
                          ,[AccessIndex] = '" + IndexValue + @"'
                          ,[Remarks] = '" + Remarks + @"'
                     WHERE NotesHandle= '" + NotesHandle + @"'");
        }
        public int DeleteNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [Note_Handle]
                     WHERE  NotesHandle= '" + NotesHandle + @"'");
        }
        public DataTable Insertdata()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@NotesHandle", NotesHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@Title", Title));
            sqlParamater.Add(new SqlParameter("@Description", Description));
            sqlParamater.Add(new SqlParameter("@User", @User));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "[sp_InsertNotes]", sqlParamater.ToArray()).Tables[0];
        }
        public int UpdateNotes()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [Note_Notes]
                       SET [Title] = '" + Title + @"'
                          ,[Description] = '" + Description + @"'
                     WHERE NotesId= '" + NoteID + @"'");
        }

        public int DeleteNotes()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [Note_Notes]
                     WHERE  NotesId= '" + NoteID + @"'");
        }
    }
}