using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BusinessLogic
{
    public class Product
    {
        public static string Con = ConfigurationManager.AppSettings["PackageConnectionLive"];

        public int PkNo { get; set; }
        public string ReceiptNumber { get; set; }
        public string ISGECPoNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string PackingListDate { get; set; }
        public decimal NetWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public string TranspoterName { get; set; }
        public string vehicleNumber { get; set; }
        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public int productId { get; set; }
        public string ISGECItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitWeight { get; set; }
        public decimal TotalWeight { get; set; }
        public string DrawingId { get; set; }
        public string RevisionNumber { get; set; }
        public string PackageType { get; set; }
        public string PackageMarks { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string UOMDimension { get; set; }
        public int SerialNumber { get; set; }

        public DataTable CreateTableForPackageDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderNumber", typeof(string));
            dt.Columns.Add("PkNumber", typeof(int));
            dt.Columns.Add("ReceiptNumber", typeof(string));
            dt.Columns.Add("SerialNumber", typeof(int));
            dt.Columns.Add("ISGECItemCode", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("UnitWeight", typeof(decimal));
            dt.Columns.Add("TotalWeight", typeof(decimal));
            dt.Columns.Add("DrawingId", typeof(string));
            dt.Columns.Add("RevisionNumber", typeof(string));
            dt.Columns.Add("PackageType", typeof(string));
            dt.Columns.Add("PackageMarks", typeof(string));
            dt.Columns.Add("Length", typeof(decimal));
            dt.Columns.Add("Width", typeof(decimal));
            dt.Columns.Add("Height", typeof(decimal));
            dt.Columns.Add("UOMDimension", typeof(string));
            dt.Columns.Add("Refcntd", typeof(string));
            dt.Columns.Add("Refcntu", typeof(string));
            return dt;
        }

        public DataTable GetRecNoFrom(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select count(t_rcno) as RCCount from twhinh312" + cmpNo + " where t_rcno='" + ReceiptNumber + "'").Tables[0];
        }

        public DataTable GetPKNO(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, " SELECT ISNULL(MAX(t_pkno),0) + 1 as PkNo FROM ttdisg017" + cmpNo + "").Tables[0];
        }
        public int InsertProduct(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO ttdisg017" + cmpNo + @"(t_pkno,t_srno,t_pkgn,t_rcno,t_orno,t_isup,t_pkdt,t_ntwt,t_tnam,t_vhno,t_lrno,t_lrdt,t_Refcntd,t_Refcntu,t_grwt)
                      VALUES('" + PkNo + "','0','0','0','" + ISGECPoNo + "','" + InvoiceNumber + "','" + PackingListDate + "','" + NetWeight + "','" + TranspoterName + "','" + vehicleNumber + "','" + LRNumber + "','" + LRDate + "','0','0','" + GrossWeight + "')");
        }
        public int InsertProductDetails(DataTable dtPackage, string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@dtPackage", dtPackage));
            sqlParameter.Add(new SqlParameter("@TableName", "ttdisg018" + cmpNo));
            return SqlHelper.ExecuteNonQuery(Con, CommandType.StoredProcedure, "[spInsertProductDetails]", sqlParameter.ToArray());
        }
        public void DeleteProduct(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "DELETE from ttdisg018" + cmpNo + " where  t_orno='" + ISGECPoNo + "' and t_pkno =(select t_pkno from ttdisg017" + cmpNo + " where t_orno='" + ISGECPoNo + "' and t_isup='" + InvoiceNumber + "' and t_pkdt='" + PackingListDate + "')");
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "DELETE from ttdisg017" + cmpNo + " where  t_orno='" + ISGECPoNo + "' and t_isup='" + InvoiceNumber + "' and t_pkdt='" + PackingListDate + "'");
        }

        public DataTable GetPakingListNO(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_pkno,(Convert(nvarchar(50),t_isup)+'('+Convert(VARCHAR(10),t_pkdt,103)+')') as Invoice  from ttdisg017" + cmpNo + " where t_orno='" + ISGECPoNo + "'").Tables[0];
        }
        public DataTable GetProducts(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_rcno as ReceiptNumber,t_orno as ISGECPoNo,t_isup as InvoiceNumber,t_pkdt as PackingListDate,t_ntwt as NetWeight,t_tnam as TranspoterName,t_vhno as vehicleNumber,t_lrno as LRNumber,t_lrdt as LRDate,t_Refcntd,t_Refcntu FROM ttdisg017" + cmpNo + " WHERE t_pkno='" + PkNo + "'").Tables[0];
        }
        public DataTable GetProductDetails(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select t_rcno as ReceiptNumber,t_rcln as SerialNumber,t_citm as ISGEC_ItemCode,t_cuni as UOM,t_qnty as Quantity,
                        t_uwgt as UnitWeight,t_twgt as TotalWeight,t_docn as DrawingId,t_revn as RevisionNumber,t_ptyp as PackageType,
                        t_pmrk as PackageMarks,t_leng as Length,t_widt as Width,t_hght as Height,t_unit as UOMDimension,t_Refcntd,t_Refcntu
                        from ttdisg018" + cmpNo + " WHERE t_pkno='" + PkNo + "' and t_orno='"+ ISGECPoNo + "'").Tables[0];
        }
        public DataTable GetAllProductDetails(string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select t_rcno as ReceiptNumber,t_rcln as SerialNumber,t_citm as ISGEC_ItemCode,t_cuni as UOM,t_qnty as Quantity,
                        t_uwgt as UnitWeight,t_twgt as TotalWeight,t_docn as DrawingId,t_revn as RevisionNumber,t_ptyp as PackageType,
                        t_pmrk as PackageMarks,t_leng as Length,t_widt as Width,t_hght as Height,t_unit as UOMDimension,t_Refcntd,t_Refcntu
                        from ttdisg018200
                     UNION ALL         
                         select t_rcno as ReceiptNumber,t_rcln as SerialNumber,t_citm as ISGEC_ItemCode,t_cuni as UOM,t_qnty as Quantity,
                         t_uwgt as UnitWeight,t_twgt as TotalWeight,t_docn as DrawingId,t_revn as RevisionNumber,t_ptyp as PackageType,
                         t_pmrk as PackageMarks,t_leng as Length,t_widt as Width,t_hght as Height,t_unit as UOMDimension,t_Refcntd,t_Refcntu
                         from ttdisg018700               
                ").Tables[0];
        }

        public DataTable GetReceiptStatus(string RcptNo, string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_stat from twhinh310" + cmpNo + "  Where t_rcno='" + RcptNo + "'").Tables[0];
        }

        public DataTable GetRecNoToUpdate(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_rcno,t_item,t_qnty,t_wght,t_orno,t_qoor  from ttdisg005" + cmpNo + " where t_rcno='" + ReceiptNumber + "' and t_item='         " + ISGECItemCode + "'").Tables[0];
        }
        public DataTable GetTotalQuantyByOrdr(string OrderNo, string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select SUM(t_qoor) from ttdisg005" + cmpNo + " ,twhinh312" + cmpNo + @"
            where ttdisg005" + cmpNo + @".t_rcno = twhinh312" + cmpNo + @".t_rcno
            and ttdisg005" + cmpNo + @".t_rcln = twhinh312" + cmpNo + @".t_rcln
            and ttdisg005" + cmpNo + @".t_item = '         "+ISGECItemCode+"' and twhinh312" + cmpNo + @".t_orno = '"+ISGECPoNo+"'").Tables[0]; // and t_rcno='" +ReceiptNumber+"'
        }
        public int UpdatePackage(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update ttdisg017" + cmpNo + " Set t_rcno='" + ReceiptNumber + "' where t_orno='" + ISGECPoNo + "'");
            SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update ttdisg018" + cmpNo + " Set t_rcno='" + ReceiptNumber + "' where t_orno='" + ISGECPoNo + "' and t_pkno ='" + PkNo + "'");
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update ttdisg005" + cmpNo + "  SET t_qoor='" + Quantity + "' , t_acht='" + TotalWeight + "', t_slct='1' WHERE t_rcno='" + ReceiptNumber + "' and t_item='         " + ISGECItemCode + "'");

        }
        public DataTable GetRcNoForBind(string cmpNo, string env)
        {
            if (env == "P")
            {
                Con = SqlConn.ConLive;
            }
            else if (env == "T")
            {
                Con = SqlConn.ConTest;
            }
            else
            {
                Con = "";
            }
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_rcno from ttdisg017" + cmpNo + "").Tables[0];
        }
    }
}
