using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcustGL_KTP
{
    public class XcustGlIntTblDB
    {
        ConnectDB conn;
        public XcustGLIntTbl xCGLIT;
        public XcustGlIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCGLIT = new XcustGLIntTbl();
            xCGLIT.STATUS_CODE = "STATUS_CODE";
            xCGLIT.LEDGER_ID = "LEDGER_ID";
            xCGLIT.ACCOUNTING_DATE = "ACCOUNTING_DATE";
            xCGLIT.JOURNAL_SOURCE = "JOURNAL_SOURCE";
            xCGLIT.JOURNAL_CATEGORY = "JOURNAL_CATEGORY";
            xCGLIT.CURRENCY_CODE = "CURRENCY_CODE";
            xCGLIT.CREATED_DATE = "CREATED_DATE";
            xCGLIT.ACTUAL_FLAG = "ACTUAL_FLAG";
            xCGLIT.SEGMENT1 = "SEGMENT1";
            xCGLIT.SEGMENT2 = "SEGMENT2";
            xCGLIT.SEGMENT3 = "SEGMENT3";
            xCGLIT.SEGMENT4 = "SEGMENT4";
            xCGLIT.SEGMENT5 = "SEGMENT5";
            xCGLIT.SEGMENT6 = "SEGMENT6";
            xCGLIT.SEGMENT7 = "SEGMENT7";
            xCGLIT.SEGMENT8 = "SEGMENT8";
            xCGLIT.SEGMENT9 = "SEGMENT9";
            xCGLIT.SEGMENT10 = "SEGMENT10";

            xCGLIT.ENTERED_DR = "ENTERED_DR";
            xCGLIT.ENTERED_CR = "ENTERED_CR";

            xCGLIT.REFERENCE1 = "REFERENCE1";
            xCGLIT.REFERENCE2 = "REFERENCE2";
            xCGLIT.REFERENCE3 = "REFERENCE3";
            xCGLIT.REFERENCE4 = "REFERENCE4";
            xCGLIT.REFERENCE5 = "REFERENCE5";
            xCGLIT.REFERENCE6 = "REFERENCE6";
            xCGLIT.REFERENCE7 = "REFERENCE7";
            xCGLIT.REFERENCE8 = "REFERENCE8";
            xCGLIT.REFERENCE9 = "REFERENCE9";
            xCGLIT.REFERENCE10 = "REFERENCE10";

            xCGLIT.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCGLIT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCGLIT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCGLIT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCGLIT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCGLIT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCGLIT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCGLIT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCGLIT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCGLIT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCGLIT.ATTRIBUTE10 = "ATTRIBUTE10";

           
            xCGLIT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCGLIT.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            xCGLIT.CREATION_DATE = "CREATION_DATE";
            xCGLIT.CREATE_BY = "CREATE_BY";
            xCGLIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCGLIT.IMPORT_SOURCE = "IMPORT_SOURCE";
            
            xCGLIT.pkField = "";
            xCGLIT.table = "XCUST_GL_INT_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCGLIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }

        public String insert(XcustGLIntTbl g)
        {
            String sql = "", chk = "";
            try
            {
               // String seq = "000000" + genRequisition_Number();
                

                sql = "Insert Into " + xCGLIT.table + "(" + xCGLIT.STATUS_CODE + "," + 
                xCGLIT.LEDGER_ID + "," +
                xCGLIT.ACCOUNTING_DATE + "," +
                xCGLIT.JOURNAL_SOURCE + "," +
                xCGLIT.JOURNAL_CATEGORY + "," +
                xCGLIT.CURRENCY_CODE + "," +
                xCGLIT.CREATED_DATE + "," +
                xCGLIT.ACTUAL_FLAG + "," +
                xCGLIT.SEGMENT1 + "," +
                xCGLIT.SEGMENT2 + "," +
                xCGLIT.SEGMENT3 + "," +
                xCGLIT.SEGMENT4 + "," +
                xCGLIT.SEGMENT5 + "," +
                xCGLIT.SEGMENT6 + "," +
                xCGLIT.SEGMENT7 + "," +
                xCGLIT.SEGMENT8 + "," +
                xCGLIT.SEGMENT9 + "," +
                xCGLIT.SEGMENT10 + "," +
                xCGLIT.ENTERED_DR + "," +
                xCGLIT.ENTERED_CR + "," +
                xCGLIT.REFERENCE1 + "," +
                xCGLIT.REFERENCE2 + "," +
                xCGLIT.REFERENCE3 + "," +
                xCGLIT.REFERENCE4 + "," +
                xCGLIT.REFERENCE5 + "," +
                xCGLIT.REFERENCE6 + "," +
                xCGLIT.REFERENCE7 + "," +
                xCGLIT.REFERENCE8 + "," +
                xCGLIT.REFERENCE9 + "," +
                xCGLIT.REFERENCE10 + "," +
                xCGLIT.ATTRIBUTE_CATEGORY + ","+
                xCGLIT.ATTRIBUTE1 + "," +
                xCGLIT.ATTRIBUTE2 + "," +
                xCGLIT.ATTRIBUTE3 + "," +
                xCGLIT.ATTRIBUTE4 + "," +
                xCGLIT.ATTRIBUTE5 + "," +
                xCGLIT.ATTRIBUTE6 + "," +
                xCGLIT.ATTRIBUTE7 + "," +
                xCGLIT.ATTRIBUTE8 + "," +
                xCGLIT.ATTRIBUTE9 + "," +
                xCGLIT.ATTRIBUTE10 + "," +
                xCGLIT.LAST_UPDATE_DATE + "," +
                xCGLIT.LAST_UPDATE_BY + "," +
                xCGLIT.CREATION_DATE + "," +
                xCGLIT.CREATE_BY + "," +
                xCGLIT.PROCESS_FLAG + "," +
                xCGLIT.IMPORT_SOURCE + "," +
                    ") " +
                    "Values('" + g.STATUS_CODE + "," +
                g.LEDGER_ID + "," +
                g.ACCOUNTING_DATE + "," +
                g.JOURNAL_SOURCE + "," +
                g.JOURNAL_CATEGORY + "," +
                g.CURRENCY_CODE + "," +
                g.CREATED_DATE + "," +
                g.ACTUAL_FLAG + "," +
                g.SEGMENT1 + "," +
                g.SEGMENT2 + "," +
                g.SEGMENT3 + "," +
                g.SEGMENT4 + "," +
                g.SEGMENT5 + "," +
                g.SEGMENT6 + "," +
                g.SEGMENT7 + "," +
                g.SEGMENT8 + "," +
                g.SEGMENT9 + "," +
                g.SEGMENT10 + "," +
                g.ENTERED_DR + "," +
                g.ENTERED_CR + "," +
                g.REFERENCE1 + "," +
                g.REFERENCE2 + "," +
                g.REFERENCE3 + "," +
                g.REFERENCE4 + "," +
                g.REFERENCE5 + "," +
                g.REFERENCE6 + "," +
                g.REFERENCE7 + "," +
                g.REFERENCE8 + "," +
                g.REFERENCE9 + "," +
                g.REFERENCE10 + "," +
                g.ATTRIBUTE_CATEGORY + "," +
                g.ATTRIBUTE1 + "," +
                g.ATTRIBUTE2 + "," +
                g.ATTRIBUTE3 + "," +
                g.ATTRIBUTE4 + "," +
                g.ATTRIBUTE5 + "," +
                g.ATTRIBUTE6 + "," +
                g.ATTRIBUTE7 + "," +
                g.ATTRIBUTE8 + "," +
                g.ATTRIBUTE9 + "," +
                g.ATTRIBUTE10 + "," +
                g.LAST_UPDATE_DATE + "," +
                g.LAST_UPDATE_BY + "," +
                g.CREATION_DATE + "," +
                g.CREATE_BY + "," +
                g.PROCESS_FLAG + "," +
                g.IMPORT_SOURCE + "," +
                    "') ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po");
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }

    }
}
