using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AttachmentApi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AttachmentApi.svc or AttachmentApi.svc.cs at the Solution Explorer and start debugging.
    public class AttachmentApi : IAttachmentApi
    {
        public string Attachments(string AthHandleS, string AthHandleT, string IndexS, string IndexT) //string AthHandleS, string AthHandleT, string IndexS, string IndexT
        {
         //   List<AttachmentCls> lst = new List<AttachmentCls>();
            try
            {
                AttachmentCls objData = new AttachmentCls();
                DataTable dt = objData.GetAttachment(AthHandleS, IndexS);
                foreach (DataRow dr in dt.Rows)
                {
                    //objData = new AttachmentCls();
                    //objData.DocumentId = dr["DocumentId"].ToString();
                    //objData.IndexValue = dr["IndexValue"].ToString();
                    //objData.AttachmentHandle = dr["AttachmentHandle"].ToString();
                    //objData.FileName = dr["FileName"].ToString();
                    //objData.PurposeCode = dr["PurposeCode"].ToString();
                    //lst.Add(objData);

                    AttachmentCls objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.AttachmentHandle = AthHandleT;
                    objAttachmentcls.IndexValue = IndexT;
                    objAttachmentcls.PurposeCode = dr["t_prcd"].ToString();// Request.QueryString["PurposeCode"];
                    objAttachmentcls.AttachedBy = dr["t_atby"].ToString(); 
                    objAttachmentcls.FileName = dr["t_fnam"].ToString();
                    objAttachmentcls.LibraryCode = dr["t_lbcd"].ToString();
                    objAttachmentcls.DocumentId = dr["t_dcid"].ToString();

                    DataTable dtDocID = objAttachmentcls.Insertdata();
                    //dtDocID.Rows
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
    }
}
