using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IRHeadReport
{
    public class ReportClass
    {
        public static string ConTest = ConfigurationManager.AppSettings["Connection45"];
        public static string ConLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string Con150 = ConfigurationManager.AppSettings["Connection150"];
        public static string Con = ConfigurationManager.AppSettings["ConnectionLive"];


        public String IRNo { get; set; }

        public string Environment(string env)
        {
            if (env == "P")
            {
                Con = ConLive;
            }
            else if (env == "T")
            {
                Con = ConTest;
            }
            else
            {
                Con = "";
            }

            return Con;
        }
        public DataTable GetIRData(string CmpId,string Connection)
        {
            return SqlHelper.ExecuteDataset(Connection, CommandType.Text, @"SELECT distinct t_ninv as IRNO,t_ifbp as BussinessPartner,t_nama as Name,
                        t_amth_1 as IRAmount, t_cdf_irdt as IrDate,
                        t_isup as SupInvoice, t_invd as SupDAte, t_cdf_cprj as ProjectCode, t_cdf_pono as PurchagrOrNo,
                        t_dsca as ProjectDesc, T4.t_rcno as RecNo,
                        (select SUM(t_damt) FROM ttdpur406200 where t_dino = '" + IRNo + @"') as PurAmount
                        FROM ttfacp100" + CmpId + @"  T1
                        LEFT JOIN ttccom100" + CmpId + @" T2 ON T2.t_bpid = T1.t_ifbp
                        INNER JOIN ttcmcs052" + CmpId + @" T3 ON T3.t_cprj = T1.t_cdf_cprj
                        INNER JOIN ttdpur406" + CmpId + @" T4 ON T4.t_dino = CAST(T1.t_ninv AS varchar(10))
                        WHERE t_ninv = '" + IRNo + "'").Tables[0];
        }
        public DataTable GetCargoType150()
        {
            return SqlHelper.ExecuteDataset(Con150, CommandType.Text, @"SELECT T1.CargoTypeId ,T2.Description as CargoType,T3.Description as ShippingLineName,T1.MBLNo,BLNumber
                    FROM ELOG_IRBL T1
                    INNER JOIN ELOG_CargoTypes T2 ON T2.CargoTypeId=T1.CargoTypeId
                    INNER JOIN ELOG_Carriers T3 ON T3.CarrierID=T1.CarrierID and T3.ShipmentModeID=T1.ShipmentModeID
                    INNER JOIN ELOG_BLHeader T4 ON T4.BLID=T1.BLID
                    WHERE IRNo='" + IRNo + "'").Tables[0];
        }
        public DataTable GetIrDetails150()
        {
                return SqlHelper.ExecuteDataset(Con150, CommandType.Text, @"SELECT IRNO  as ReferemceIRNo,T2.Description as ChargeHead,T3.description as ChargeheadDiscription,Amount
                FROM ELOG_IRBLDetails T1
                INNER JOIN ELOG_ChargeTypes T2 ON T2.ChargeTypeID=T1.ChargeTypeID
                INNER JOIN ELOG_ChargeCodes T3 ON T3.ChargeCodeID=T1.ChargeCodeID
                WHERE IRNO='" + IRNo + "'").Tables[0];
        }
        public DataTable GetIrDetailsByBLID150()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@IRNO", IRNo));
            return SqlHelper.ExecuteDataset(Con150, CommandType.StoredProcedure, @"sp_GetIRDetailsBLNO", sqlParamater.ToArray()).Tables[0];
        }
    }
}
