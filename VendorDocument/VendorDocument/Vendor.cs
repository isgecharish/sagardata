using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Configuration;

namespace VendorDocument
{
    public class Vendor
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string Conlocal = ConfigurationManager.AppSettings["localCon"];
        public DataTable GetVendordetails(string ReceiptNo,string RevisionNo)
        {
           return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select SerialNo,ItemNo,UploadNo,DocSerialNo,DocumentID,DocumentRev,DocumentDescription,ReceiptNo,
                        RevisionNo,FileName,DiskFileName 
                        FROM PAK_POLineRecDoc WHERE ReceiptNo='"+ ReceiptNo + "' and RevisionNo='"+ RevisionNo + "'").Tables[0];
        }

        public void Insertdata(string Path)
        {
            SqlHelper.ExecuteNonQuery(Conlocal, CommandType.Text, "Insert into Employee(Path) values('" + Path + "')");
        }
        public DataTable GetPAthTest()
        {
           return SqlHelper.ExecuteDataset(Conlocal, CommandType.Text, "Select Path From Employee where Name='RahulR'").Tables[0];
        }
    }
}