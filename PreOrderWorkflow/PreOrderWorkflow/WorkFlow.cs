using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PreOrderWorkflow
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
        #region Methods
        public DataTable GetWFById()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,EmployeeName,RandomNo
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE WFID='" + WFID + "'").Tables[0]; //
        }
        public DataTable GetWFBY_Status()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,EmployeeName
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE Wf_Status in(" + WF_Status + ")  and WF.Buyer='" + UserId + "' order by WFID desc").Tables[0]; //
        }
        public DataTable GetWFBYForUser()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,EmployeeName
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE WF.UserId='" + UserId + "' and Parent_WFID=0 order by WFID desc").Tables[0]; //
        }
        public DataTable GetRaisedEnqiry()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,EmployeeName
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE WF.Buyer='" + UserId + "' and Parent_WFID='" + Parent_WFID + "' order by Parent_WFID desc").Tables[0]; //
        }
        public DataTable GetWFByParentId()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,EmployeeName
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE Parent_WFID='" + Parent_WFID + "'").Tables[0]; //
        }
        public DataTable GetHistory()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"SELECT [WF_HistoryID]
                                      ,[WFID]
                                      ,[WFID_SlNo]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,[Buyer]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,[Notes]
                                  FROM [WF1_PreOrder_History]
                                WHERE  WFID='" + WFID + "'").Tables[0]; //
        }
        public DataTable InsertPreOrderData()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@Parent_WFID", Parent_WFID));
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@Element", Element));
            sqlParamater.Add(new SqlParameter("@SpecificationNo", SpecificationNo));
            sqlParamater.Add(new SqlParameter("@Buyer", Buyer));
            sqlParamater.Add(new SqlParameter("@UserId", UserId));
            sqlParamater.Add(new SqlParameter("@WF_Status", WF_Status));

            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_Insert_PreOrder_Workflow", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable InserPreOrderHistory()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@WFID", WFID));
            sqlParamater.Add(new SqlParameter("@Parent_WFID", Parent_WFID));
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@Element", Element));
            sqlParamater.Add(new SqlParameter("@SpecificationNo", SpecificationNo));
            sqlParamater.Add(new SqlParameter("@Buyer", Buyer));
            sqlParamater.Add(new SqlParameter("@WF_Status", WF_Status));
            sqlParamater.Add(new SqlParameter("@UserId", UserId));
            sqlParamater.Add(new SqlParameter("@Supplier", Supplier));
            sqlParamater.Add(new SqlParameter("@SupplierName", SupplierName));
            sqlParamater.Add(new SqlParameter("@Notes", Notes));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_Insert_PreOrder_Workflow_History", sqlParamater.ToArray()).Tables[0];
        }
        public int UpdateWFPreOrder()
        {

            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [IJTPerks].[dbo].[WF1_PreOrder]
            SET [Project] = '" + Project + "',[Element] = '" + Element + "',[SpecificationNo] = '" + SpecificationNo + "',[Buyer] = '" + Buyer + @"'
                          ,[WF_Status] = '" + WF_Status + @"'                                                
                          ,[Supplier] = '" + Supplier + @"'
                          WHERE [WFID] = '" + WFID + @"'");
            //
        }
        public int UpdateStatusWFPreOrder_History()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [IJTPerks].[dbo].[WF1_PreOrder_History]
            SET           [WF_Status] = '" + WF_Status + @"'
                          WHERE [WFID] = '" + WFID + @"' and [WFID_SlNo]='" + SLNO_WFID + "'");

            //'" + UserId + @"'     
        }
        public int UpdateWFPreOrder_History()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [IJTPerks].[dbo].[WF1_PreOrder_History]
            SET [Project] = '" + Project + "',[Element] = '" + Element + "',[SpecificationNo] = '" + SpecificationNo + "',[Buyer] = '" + Buyer + @"'
                          ,[WF_Status] = '" + WF_Status + @"'
                          ,[Supplier] = '" + Supplier + @"'
                          ,[Notes] = '" + Notes + @"'
                          WHERE [WFID] = '" + WFID + @"' and [WFID_SlNo]='" + SLNO_WFID + "'");

            //'" + UserId + @"'     
        }
        public int Delete_PreOrder()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "DELETE from Wf1_PreOrder WHERE WFID='" + WFID + "'");
        }
        public int Delete_PreOrderHistory()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "DELETE from WF1_PreOrder_History WHERE WFID='" + WFID + "'");
        }
        public int UpdateWF_Status()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update WF1_PreOrder set [WF_Status]='" + WF_Status + "',DateTime=getDate()  Where [WFID]='" + WFID + "'");
        }
        public int UpdateEnquiryRaised()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update WF1_PreOrder set [WF_Status]='" + WF_Status + "',Supplier='" + Supplier + "',SupplierName='" + SupplierName + "',DateTime=getDate(),RandomNo='" + RandomNo + "'  Where [WFID]='" + WFID + "'");
        }
        public DataTable GetWFID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "SELECT [WFID] FROM [WF1_PreOrder] where [Parent_WFID]='" + Parent_WFID + "'").Tables[0];
        }
        public DataTable GetWFHID(int ID)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select  Max(WFID_SlNo) as WFSLNO from WF1_PreOrder_History WHERE WFID=" + ID + "").Tables[0];
        }
        // --------------  129 server-------------
        public DataTable GetProject()
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select ProjectCode,ProjectName from LN_ProjectMaster").Tables[0]; //
        }
        public DataTable GetProjectName(string Project)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select ProjectCode,ProjectName from LN_ProjectMaster where ProjectCode='" + Project + "'").Tables[0]; //
        }
        public DataTable GetProjectElement(string Project)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select Project,Element,ElementDescription from LN_ProjectElements where Project='" + Project + "'").Tables[0]; //
        }
        public DataTable GetProjectElementFDesc(string Project, string Element)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select Element,ElementDescription from LN_ProjectElements where Project='" + Project + "' and Element='" + Element + "'").Tables[0]; //
        }
        public DataTable GetProjectSpecification(string Project)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select Project,DocumentID,DocumentDescription from LN_PMDL where Project='" + Project + "'").Tables[0]; //
        }
        public DataTable GetProjectSpecificationDesc(string Project, string DocId)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select DocumentID,DocumentDescription from LN_PMDL where Project='" + Project + "' and DocumentID='" + DocId + "'").Tables[0]; //
        }
        public DataTable GetProjectSpecificationMethod(string Doc)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, @"select Project,DocumentID,DocumentDescription from LN_PMDL where DocumentID Like '" + Doc + "%' or DocumentDescription Like '" + Doc + "%'").Tables[0]; //
        }
        public DataTable GetUser(string UserID)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName  from HRM_Employees where EmployeeName Like '" + UserID + "%' or CardNo Like '" + UserID + "%'").Tables[0];
        }
        public DataTable GetMAilID(string UserID)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName,EMailid  from HRM_Employees where CardNo= '" + UserID + "'").Tables[0];
        }
        public DataTable GetSupplier()
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, "select SupplierCode,SupplierName from LN_V_SupplierMaster where SupplierCode Like '" + Supplier + "%' or SupplierName Like '" + Supplier + "%'").Tables[0];
        }
        public DataTable GetSupplierMailId(string SupplierId)
        {
            return SqlHelper.ExecuteDataset(Con129, CommandType.Text, "select SupplierCode,SupplierName,EMail from LN_V_SupplierMaster where SupplierCode = '" + SupplierId + "'").Tables[0];
        }
        public DataTable GetPath()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            //  sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            return SqlHelper.ExecuteDataset(Con129, CommandType.StoredProcedure, "sp_GetAttachmentPath", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable InsertAttachmentdata()
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

        #endregion
    }
}