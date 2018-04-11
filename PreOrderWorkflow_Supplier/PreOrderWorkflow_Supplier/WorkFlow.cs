using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PreOrderWorkflow_Supplier
{
    public class WorkFlow
    {
        public static string Con = ConfigurationManager.AppSettings["Connection"];
        public static string Con129 = ConfigurationManager.AppSettings["ConnectionLive"];
        #region Properties
        public int WFID { get; set; }
        public int WFIDHistoryId { get; set; }
        public int Parent_WFID { get; set; }
        public int SLNO_WFID { get; set; }
        public string Project { get; set; }
        public string Element { get; set; }
        public string SpecificationNo { get; set; }
        public string Buyer { get; set; }
        public string WF_Status { get; set; }
        public string UserId { get; set; }
        public string Supplier { get; set; }
        public string SupplierName { get; set; }
        public string Notes { get; set; }
        public string AttachmentId { get; set; }
        public string AthhFile { get; set; }
        public string IndexValue { get; set; }
        public string AttachmentHandle { get; set; }
        public string PurposeCode { get; set; }
        public string FileName { get; set; }
        public string LibraryCode { get; set; }
        public string AttachedBy { get; set; }
        public string RandomNo { get; set; }
        #endregion
        public DataTable GetWFById()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select WF.[WFID]
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
                                INNER JOIN [WF1_PreOrder_History] WFH ON WFH.WFID=WF.WFID
                                WHERE RandomNo='"+RandomNo+"'").Tables[0]; //
        }

        public DataTable GetMAilID(string UserID)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName,EMailid  from HRM_Employees where CardNo= '" + UserID + "'").Tables[0];
        }


        public DataTable GetPath()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            //  sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            return SqlHelper.ExecuteDataset(Con129, CommandType.StoredProcedure, "sp_GetAttachmentPath", sqlParamater.ToArray()).Tables[0];
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
            return SqlHelper.ExecuteDataset(Con129, CommandType.StoredProcedure, "sp_InsertAttachment", sqlParamater.ToArray()).Tables[0];
        }

        public DataTable GetAttachments()
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"
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
                                      WHERE t_hndl in ("+ AttachmentHandle + ") and t_indx in(" + IndexValue + ")").Tables[0]; //
        }

        public DataTable GetNoteID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select NotesId from Note_Notes where IndexValue='"+IndexValue+"'").Tables[0]; //
        }
    }
}