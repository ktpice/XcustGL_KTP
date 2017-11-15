using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcustGL_KTP
{
    public class XcustMmxDusIntTblDB
    {
        public XcustMmxDusIntTbl xCMDIT;
        ConnectDB conn;
        private InitC initC;
        public XcustMmxDusIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCMDIT = new XcustMmxDusIntTbl();
            xCMDIT.FILE_NAME = "FILE_NAME";
            xCMDIT.STORE_CODE = "STORE_CODE";
            xCMDIT.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCMDIT.INV_CODE = "INV_CODE";
            xCMDIT.TRAN_DATE = "TRAN_DATE";
            xCMDIT.THEO_USAGE = "THEO_USAGE";
            xCMDIT.THEO_COST = "THEO_COST";
            xCMDIT.ACT_USAGE = "ACT_USAGE";
            xCMDIT.FIN_WASTE = "FIN_WASTE";
            xCMDIT.RAW_WASTE = "RAW_WASTE";   
            xCMDIT.POST_INTERVAL = "POST_INTERVAL";
            xCMDIT.FILE_TYPE = "FILE_TYPE";
            xCMDIT.VALIDATE_FLAG = "VALIDATE_FLAG";
            xCMDIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCMDIT.ERROR_MSG = "ERROR_MSG";
            xCMDIT.creation_by = "creation_by";
            xCMDIT.creation_date = "creation_date";
            xCMDIT.last_update_by = "last_update_by";
            xCMDIT.last_update_date = "last_update_date";

            xCMDIT.table = "xcust_mmx_dus_int_tbl";
            xCMDIT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMDIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCMDIT.FILE_NAME + " From " + xCMDIT.table + " Group By " + xCMDIT.FILE_NAME;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMDIT.table + " Where " + xCMDIT.FILE_NAME + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp()
        {
            String sql = "Delete From " + xCMDIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public String dateYearShortToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + "-" + month + "-" + day;

            return chk;
        }
        public String dateYearShortToDBTemp(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + month + day;

            return chk;
        }

        //insert Bulk
        public void insertBluk(List<String> mmx, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            String tranDate = "";
            if (host == "kfc_po")
            {
                ConnectionString = conn.connKFC.ConnectionString;
            }
            StringBuilder sql = new StringBuilder();
            pB1.Minimum = 1;
            pB1.Maximum = mmx.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in mmx)
                {
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split(',');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    tranDate = dateYearShortToDBTemp(aaa[4]);  //array index4 = column5
               
                    sql.Append("Insert Into ").Append(xCMDIT.table).Append(" (").Append(xCMDIT.FILE_NAME).Append(",").Append(xCMDIT.STORE_CODE).Append(",").Append(xCMDIT.SUPPLIER_CODE)
                        .Append(",").Append(xCMDIT.INV_CODE).Append(",").Append(xCMDIT.TRAN_DATE).Append(",").Append(xCMDIT.THEO_USAGE)
                        .Append(",").Append(xCMDIT.THEO_COST).Append(",").Append(xCMDIT.ACT_USAGE).Append(",").Append(xCMDIT.FIN_WASTE)
                        .Append(",").Append(xCMDIT.RAW_WASTE).Append(",").Append(xCMDIT.POST_INTERVAL).Append(",").Append(xCMDIT.FILE_TYPE).Append(",")
                        .Append(",").Append(xCMDIT.VALIDATE_FLAG).Append(",").Append(xCMDIT.PROCESS_FLAG).Append(",").Append(xCMDIT.ERROR_MSG)
                        .Append(xCMDIT.creation_by).Append(",").Append(xCMDIT.creation_date).Append(xCMDIT.last_update_date).Append(xCMDIT.last_update_by).Append(" ")
                        .Append(") Values ('")
                        .Append(aaa[0]).Append("','").Append(aaa[1]).Append("','").Append(aaa[2])
                        .Append("','").Append(aaa[3]).Append("','").Append(tranDate).Append("','").Append(aaa[5])
                        .Append("','").Append(aaa[6]).Append("','").Append(aaa[7]).Append("','").Append(aaa[8])
                        .Append("','").Append(aaa[9]).Append("','").Append(aaa[10]).Append("','").Append(",'").Append(aaa[11])
                        .Append("','").Append(validateFlag).Append("','").Append(processFlag).Append("','").Append(errMsg)
                        .Append(createBy).Append("',").Append(createDate).Append("',").Append(lastUpdateTime).Append(last_update_by)
                        .Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }

        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host)
        {
            String sql = "";
            sql = "Update " + xCMDIT.table + " Set " + xCMDIT.VALIDATE_FLAG + "='" + flag 
                //+ "', " + xCMDIT.AGREEEMENT_NUMBER + " ='" + agreement_number + "' " +
                //"Where " + xCMDIT.po_number + " = '" + po_number + "' and " + xCMDIT.AGREEMENT_LINE_NUMBER + "='" + line_number + "'"
                ;
            conn.ExecuteNonQuery(sql.ToString(), host);

            return "";
        }

    }
}
