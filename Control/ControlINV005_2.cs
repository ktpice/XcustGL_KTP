using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XcustGL_KTP
{
    public class ControlINV005_2
    {

        static String fontName = "Microsoft Sans Serif";        //standard
        public String backColor1 = "#1E1E1E";        //standard
        public String backColor2 = "#2D2D30";        //standard
        public String foreColor1 = "#fff";        //standard
        static float fontSize9 = 9.75f;        //standard
        static float fontSize8 = 8.25f;        //standard
        public Font fV1B, fV1;        //standard
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;        //standard

        public ControlMain Cm;
        public ConnectDB conn;        //standard

        public XcustMmxDusIntTblDB xCMDITDB;
        public XcustGlIntTblDB xCGLITDB;

        public XcustBuMstTblDB xCBMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;

        private List<XcustGLIntTbl> listXcustGLIT;

        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;



        public ControlINV005_2(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }

        private void initConfig()
        {
            

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard

            xCMDITDB = new XcustMmxDusIntTblDB(conn, Cm.initC);
            xCGLITDB = new XcustGlIntTblDB(conn);

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);


            Cm.createFolderINV005_2();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard


            listXcustGLIT = new List<XcustGLIntTbl>();

            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();

        }

        /*
        * a.	ระบบ MMX จะ  SFTP file จากระบบงาน MMX และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
        * b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
        */

        public void processMMXtoErpGL(String[] fileGL, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadText rd = new ReadText();
            String[] fileGLProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
            addListView("อ่าน fileจาก" + Cm.initC.INV005_2PathInitial, "", lv1, form1);
            foreach (string aa in fileGL)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.INV005_2PathProcess + aa.Replace(Cm.initC.INV005_2PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCMDITDB.DeleteMmxTemp();//  clear temp table
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_DUS_INT_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_dus_int_tbl
            fileGLProcess = Cm.getFileinFolder(Cm.initC.INV005_2PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.INV005_2PathProcess, "", lv1, form1);
            foreach (string aa in fileGLProcess)
            {
                List<String> mmx = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                pB1.Visible = true;
                xCMDITDB.insertBluk(mmx, aa, "kfc_po", pB1);
                pB1.Visible = false;
            }
        }

        private void addListView(String col1, String col2, MaterialListView lv1, Form form1)
        {
            lv1.Items.Add(AddToList((lv1.Items.Count + 1), col1, col2));
            form1.Refresh();
        }
        private ListViewItem AddToList(int col1, string col2, string col3)
        {
            //int i = 0;
            string[] array = new string[3];
            array[0] = col1.ToString();
            //i = lv.Items.Count();
            //array[0] = lv.Items.Count();
            array[1] = col2;
            array[2] = col3;

            return (new ListViewItem(array));
        }

        /*
        * d.	จากนั้น Program จะเอาข้อมูลจาก Table XCUST_MMX_DUS_INT_TBL มาทำการ Validate 
        * e.	กรณีที่ Validate ผ่าน จะเอาข้อมูล Insert ลง table XCUST_GL_INT_TBL และ Update Validate_flag = ‘Y’
        * h.	กรณีที่ Validate ไม่ผ่าน จะะ Update Validate_flag = ‘E’ พร้อมระบุ Error Message
        */
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("อ่าน file จาก " + Cm.initC.PathProcess, "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtGroupBy = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            String buCode = "", locator = "", Org = "", subInv_code = "", currencyCode = "";
            ValidateGL vGL = new ValidateGL();
            List<ValidateGL> lVGL = new List<ValidateGL>();

            listXcustGLIT.Clear();

            getListXcSMT();
            getListXcVSMT();


            int row1 = 0;


            //Error INV005_2-009 : Invalid Currency Code
            if (!xCMTDB.validateCurrencyCodeBycurrCode(Cm.initC.CURRENCY_CODE))
            {
                chk = false;
            }

            dtGroupBy = xCMDITDB.selectMmxGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim(), "Validate", lv1, form1);
                dt = xCMDITDB.selectMmxByFilename(rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error INV005_2-010 : Invalid data type
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.THEO_USAGE].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " THEO_USAGE=" + row[xCMDITDB.xCMDIT.THEO_USAGE].ToString();
                        lVGL.Add(vGL);
                    }
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.THEO_COST].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " THEO_COST=" + row[xCMDITDB.xCMDIT.THEO_COST].ToString();
                        lVGL.Add(vGL);
                    }
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.ACT_USAGE].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " ACT_USAGE=" + row[xCMDITDB.xCMDIT.ACT_USAGE].ToString();
                        lVGL.Add(vGL);
                    }
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.FIN_WASTE].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " FIN_WASTE=" + row[xCMDITDB.xCMDIT.FIN_WASTE].ToString();
                        lVGL.Add(vGL);
                    }
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.RAW_WASTE].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " RAW_WASTE=" + row[xCMDITDB.xCMDIT.RAW_WASTE].ToString();
                        lVGL.Add(vGL);
                    }
                    chk = Cm.validateQTY(row[xCMDITDB.xCMDIT.POST_INTERVAL].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-010 : Invalid data type";
                        vGL.Validate = "row " + row1 + " POST_INTERVAL=" + row[xCMDITDB.xCMDIT.POST_INTERVAL].ToString();
                        lVGL.Add(vGL);
                    }

                    //Error INV005_2-002 : Date Format not correct 
                    chk = Cm.validateDate(row[xCMDITDB.xCMDIT.TRAN_DATE].ToString());
                    if (!chk)
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-002 : Date Format not correct ";
                        vGL.Validate = "row " + row1 + " TRAN_DATE=" + row[xCMDITDB.xCMDIT.TRAN_DATE].ToString();
                        lVGL.Add(vGL);
                    }
                    
         
                    //Error INV005_2-004 : Invalid Supplier
                    if (Cm.validateSupplierBySupplierCode(row[xCMDITDB.xCMDIT.SUPPLIER_CODE].ToString().Trim(), listXcSMT))
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-004 : Invalid Supplier ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " SUPPLIER_CODE " + row[xCMDITDB.xCMDIT.SUPPLIER_CODE].ToString().Trim();
                        lVGL.Add(vGL);
                    }

                    //Error INV005_2-011 : Invalid CHARGE_ACCOUNT_SEGMENT1
                    if (Cm.validateValueBySegment1("COMPANY RD CLOUD", "Y", "11", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = row[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "INV005_2-011 : Invalid CHARGE_ACCOUNT_SEGMENT1";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT1 ";
                        lVGL.Add(vGL);
                    }
                    //Error INV005_2-012 : Invalid CHARGE_ACCOUNT_SEGMENT2
                    if (Cm.validateValueBySegment2("STORE RD CLOUD", "Y", "00000", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-012 : Invalid CHARGE_ACCOUNT_SEGMENT2 ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT2 ";
                        lVGL.Add(vGL);
                    }
                    //Error INV005_2-013 : Invalid CHARGE_ACCOUNT_SEGMENT3
                    if (Cm.validateValueBySegment3("COMPANY RD CLOUD", "Y", "11", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = row[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-013 : Invalid CHARGE_ACCOUNT_SEGMENT3 ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT3 ";
                        lVGL.Add(vGL);
                    }
                    //Error INV005_2-014 : Invalid CHARGE_ACCOUNT_SEGMENT4
                    if (Cm.validateValueBySegment4("STORE RD CLOUD", "Y", "00000", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-014 : Invalid CHARGE_ACCOUNT_SEGMENT4 ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT4 ";
                        lVGL.Add(vGL);
                    }
                    //Error INV005_2-015 : Invalid CHARGE_ACCOUNT_SEGMENT5
                    if (Cm.validateValueBySegment5("COMPANY RD CLOUD", "Y", "11", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = row[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-015 : Invalid CHARGE_ACCOUNT_SEGMENT5 ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT5 ";
                        lVGL.Add(vGL);
                    }
                    //Error INV005_2-016 : Invalid CHARGE_ACCOUNT_SEGMENT6
                    if (Cm.validateValueBySegment6("STORE RD CLOUD", "Y", "00000", listXcVSMT))// ต้องแก้ Fix code อยู่
                    {
                        vGL = new ValidateGL();
                        vGL.Filename = rowG[xCMDITDB.xCMDIT.FILE_NAME].ToString().Trim();
                        vGL.Message = "Error INV005_2-016 : Invalid CHARGE_ACCOUNT_SEGMENT6 ";
                        vGL.Validate = "row " + row1 + " store_code=" + row[xCMDITDB.xCMDIT.STORE_CODE].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVGL.Add(vGL);
                    }

                   
                   
                }
            }
        }

        /*
         * e.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_GL_INT_TBL และ Update Validate_flag = ‘Y’
         */
        private void addXcustListGLIntTbl(DataRow row)
        {//row[dc].ToString().Trim().
            String chk = "";
            XcustGLIntTbl xCGLIT = new XcustGLIntTbl();
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");

            xCGLIT.STATUS_CODE = "NEW";
            xCGLIT.LEDGER_ID = ""; //ต้องไปหาจาก table xcust_bu_mst_tbl.legal_entity_id
            xCGLIT.ACCOUNTING_DATE = row[xCMDITDB.xCMDIT.TRAN_DATE].ToString();
            xCGLIT.JOURNAL_SOURCE = "MMX_DUS";
            xCGLIT.JOURNAL_CATEGORY = "MMX_DUS";
            xCGLIT.CURRENCY_CODE = "THB";
            xCGLIT.REFERENCE1 = "MMX_DUS" + "-" + row[xCMDITDB.xCMDIT.FILE_NAME].ToString();
            xCGLIT.REFERENCE2 = "MMX_DUS" + "-" + row[xCMDITDB.xCMDIT.FILE_NAME].ToString();
            xCGLIT.REFERENCE3 = "MMX_DUS" + "-" + row[xCMDITDB.xCMDIT.FILE_NAME].ToString()
                                 + row[xCMDITDB.xCMDIT.STORE_CODE].ToString() + "-"
                                 + row[xCMDITDB.xCMDIT.SUPPLIER_CODE].ToString() + "-"
                                 + row[xCMDITDB.xCMDIT.INV_CODE].ToString();
            xCGLIT.REFERENCE4 = "MMX_DUS" + "-" + row[xCMDITDB.xCMDIT.FILE_NAME].ToString()
                                 + row[xCMDITDB.xCMDIT.STORE_CODE].ToString() + "-"
                                 + row[xCMDITDB.xCMDIT.SUPPLIER_CODE].ToString() + "-"
                                 + row[xCMDITDB.xCMDIT.INV_CODE].ToString();
            xCGLIT.CREATED_DATE = date;
            xCGLIT.ACTUAL_FLAG = "A";
            xCGLIT.SEGMENT1 = "01";
            xCGLIT.SEGMENT2 = "000000";
            xCGLIT.SEGMENT3 = "00000";
            xCGLIT.SEGMENT4 = "000000";
            xCGLIT.SEGMENT5 = "00";
            xCGLIT.SEGMENT6 = "0000";
            xCGLIT.ENTERED_DR = row[xCMDITDB.xCMDIT.ACT_USAGE].ToString();
            //xCGLIT.ENTERED_CR = xcglit.ENTERED_CR;
            xCGLIT.ATTRIBUTE_CATEGORY = "MMX_DUS";
            xCGLIT.ATTRIBUTE1 = row[xCMDITDB.xCMDIT.SUPPLIER_CODE].ToString();
            xCGLIT.ATTRIBUTE2 = row[xCMDITDB.xCMDIT.INV_CODE].ToString();
            xCGLIT.ATTRIBUTE3 = row[xCMDITDB.xCMDIT.THEO_USAGE].ToString();
            xCGLIT.ATTRIBUTE4 = row[xCMDITDB.xCMDIT.THEO_COST].ToString();
            xCGLIT.ATTRIBUTE5 = row[xCMDITDB.xCMDIT.FIN_WASTE].ToString();
            xCGLIT.ATTRIBUTE6 = row[xCMDITDB.xCMDIT.RAW_WASTE].ToString();
            xCGLIT.ATTRIBUTE7 = row[xCMDITDB.xCMDIT.POST_INTERVAL].ToString();
            xCGLIT.IMPORT_SOURCE = Cm.initC.INV005_2ImportSource;

            listXcustGLIT.Add(xCGLIT);

        }


        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.INV005_2PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustGLIntTbl xcglit in listXcustGLIT)
            {
                String chk = xCGLITDB.insert(xcglit);
            }
        }
 

        /*
        * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
        */
        private void getListXcSMT()
        {
            listXcSMT.Clear();
            DataTable dt = xCSMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustSupplierMstTbl item = new XcustSupplierMstTbl();
                item = xCSMTDB.setData(row);
                listXcSMT.Add(item);
            }
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcVSMT()
        {
            listXcVSMT.Clear();
            DataTable dt = xCVSMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustValueSetMstTbl item = new XcustValueSetMstTbl();
                item = xCVSMTDB.setData(row);
                listXcVSMT.Add(item);
            }
        }

    }
}
