using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{
    public class Sanction
    {
        public int RequestNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProjectCode { get; set; }
        public string Activity { get; set; }
        public decimal FairAmount { get; set; }
        public decimal HotelCharges { get; set; }
        public decimal UpdatedTotalAmount { get; set; }
        public decimal LocalConveyance { get; set; }
        public string Element { get; set; }
        public string OpeningBalance { get; set; }
        public DataTable BindProducts()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "select ProjectCode,ProjectName from ProjectMaster").Tables[0];
        }

        public DataTable BindProductsServer()
        {
            return SqlHelper.ExecuteDataset(SqlConn.ProjectCon, CommandType.Text, "select t_cprj,t_dsca from ttcmcs052200  where t_cprj NOT LIKE '[0-9]%'").Tables[0];
        }
        public DataTable getMaxRequestNo()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "select Isnull(Max(RequestNumber),0)+1 as RequestNumber from BillSanction").Tables[0];
        }

        public int SaveTABillHistory()
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@RequestNumber", RequestNumber));
            sqlParameter.Add(new SqlParameter("@TotalAmount", UpdatedTotalAmount));
            sqlParameter.Add(new SqlParameter("@ProjectCode", ProjectCode));
            sqlParameter.Add(new SqlParameter("@Activity", Activity));
            sqlParameter.Add(new SqlParameter("@FairAmount", FairAmount));
            sqlParameter.Add(new SqlParameter("@HotelCharges", HotelCharges));
            sqlParameter.Add(new SqlParameter("@LocalConveyance", LocalConveyance));
            return SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.StoredProcedure, "InsertTABillSanctionHistory", sqlParameter.ToArray());
        }
        public int SaveTABill()
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@RequestNumber", SqlDbType.Int));
            sqlParameter[0].Direction = ParameterDirection.Output;
            sqlParameter.Add(new SqlParameter("@TotalAmount", TotalAmount));
            sqlParameter.Add(new SqlParameter("@ProjectCode", ProjectCode));
            sqlParameter.Add(new SqlParameter("@Activity", Activity));
            sqlParameter.Add(new SqlParameter("@FairAmount", FairAmount));
            sqlParameter.Add(new SqlParameter("@HotelCharges", HotelCharges));
            sqlParameter.Add(new SqlParameter("@LocalConveyance", LocalConveyance));

            SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.StoredProcedure, "InsertTABillSanction", sqlParameter.ToArray());
            int value = Convert.ToInt32(sqlParameter[0].Value);
            return value;
        }
        public int UpdateTABill()
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@RequestNumber", RequestNumber));
            sqlParameter.Add(new SqlParameter("@TotalAmount", TotalAmount));
            sqlParameter.Add(new SqlParameter("@ProjectCode", ProjectCode));
            sqlParameter.Add(new SqlParameter("@Activity", Activity));
            sqlParameter.Add(new SqlParameter("@FairAmount", FairAmount));
            sqlParameter.Add(new SqlParameter("@HotelCharges", HotelCharges));
            sqlParameter.Add(new SqlParameter("@LocalConveyance", LocalConveyance));
            return SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.StoredProcedure, "UpdateTABillSanction", sqlParameter.ToArray());
        }

        public int DeleteSanction(int RequestNo)
        {
            return SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.Text, "Delete FROM BillSanction WHERE RequestNumber=" + RequestNo);
        }
        public DataTable GetSanctionBills()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, @"SELECT RequestNumber,TotalAmount,ProjectName,ProjectMaster.ProjectCode,FairAmount,HotelCharges,LocalConveyance
             FROM BillSanction
             Inner Join ProjectMaster ON ProjectMaster.ProjectCode = BillSanction.ProjectCode order by RequestNumber").Tables[0];
        }
        public DataTable GetSanctionBillsHistory()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, @"select RequestNumber,SerialNo,TotalAmount,ProjectCode,Activity from BillSanctionHistory
                    Order by RequestNumber asc").Tables[0];
        }
        public DataTable GetSanctionBillsBySerial(int RequestNo)
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, @"SELECT RequestNumber,TotalAmount,ProjectName,ProjectMaster.ProjectCode,FairAmount,HotelCharges,LocalConveyance
             FROM BillSanction
             Inner Join ProjectMaster ON ProjectMaster.ProjectCode = BillSanction.ProjectCode
             Where RequestNumber=" + RequestNo).Tables[0];
        }
        public DataTable GetTotalAmount(int RequestNo)
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "Select Sum(TotalAmount) from BillSanctionHistory Where RequestNumber =" + RequestNo).Tables[0];
        }
        public DataTable GetTotalAmountByProjectCode(string PCode)
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "Select Sum(TotalAmount) from BillSanctionHistory Where ProjectCode ='" + PCode + "'").Tables[0];
        }

        public DataTable GetOpeningBalance(string ProjectFrom, string ProjectTo)
        {
            return SqlHelper.ExecuteDataset(SqlConn.ProjectCon, CommandType.Text, @"select t_cprj as ProjectCode,t_elem as Element,t_avai as OpeningBalance from ttpisg012200
                where t_elem = '99250100' and t_cprj between '" + ProjectFrom + "' and '" + ProjectTo + "'").Tables[0];
        }
        public int InsertOpeningBalance()
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@ProjectCode", ProjectCode));
            sqlParameter.Add(new SqlParameter("@Element", Element));
            sqlParameter.Add(new SqlParameter("@OpeningBalance", OpeningBalance));
            return SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.StoredProcedure, "InsertOpeningBalance", sqlParameter.ToArray());
        }

        // insert opening balance through datatable
        public int InsertBalance(DataTable dt)
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@dtBalance", dt));
            return SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.StoredProcedure, "InsertProjectOpeningBalance", sqlParameter.ToArray());
        }

        public DataTable GetAllOpeningBalance()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "SELECT ProjectCode,Element,OpeningBalanceDate,OpeningBalance,Consumption,LastConsumptionDate from ProjectSanctionBalance").Tables[0];
        }
        public DataTable GetOpeningBalanceByProjectCode()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "SELECT OpeningBalance from ProjectSanctionBalance Where ProjectCode='"+ProjectCode+"'").Tables[0];
        }
        public DataTable GetTotalConsumption()
        {
            return SqlHelper.ExecuteDataset(SqlConn.TABillCon, CommandType.Text, "Select Sum(Consumption) from projectSanctionbalance Where ProjectCode ='"+ProjectCode+"'").Tables[0];
        }
        public void UpdateConsumption(decimal Consumption)
        {

            SqlHelper.ExecuteNonQuery(SqlConn.TABillCon, CommandType.Text, "Update ProjectSanctionBalance set Consumption=" + Consumption + ",LastConsumptionDate=getdate() Where ProjectCode='" + ProjectCode + "'");
        }
    }
}
