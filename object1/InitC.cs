﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcustGL_KTP
{
    public class InitC
    {
        public String PathInitial = "", PathProcess="",PathError="",PathArchive="", BU_NAME="", APPROVER_EMAIL="", Requester="";

        public String databaseDBBIT = "bithis_demo1";             //bit
        public String hostDBBIT = "172.25.1.153";
        public String userDBBIT = "sa";
        public String passDBBIT = "Orawanhospital1*";
        public String portDBBIT = "3306";

        public String databaseDBBITDemo = "bithis_demo";             //bit demo
        public String hostDBBITDemo = "172.25.1.153";
        public String userDBBITDemo = "sa";
        public String ImportSource = "ImportSource";
        public String Company = "Company";
        public String DELIVER_TO_LOCATTION = "DELIVER_TO_LOCATTION";
        public String ORGANIZATION_code = "ORGANIZATION_code";
        public String Locator = "Locator";
        public String Subinventory_Code = "Locator";
        public String CURRENCY_CODE = "CURRENCY_CODE";
        public String PR_STATAUS = "";
        public String LINE_TYPE = "";
        public String AutoRunPO001 = "";
        public String AutoRunPO004 = "";
        public String AutoRunPO005 = "";
        public String AutoRunPO003 = "";

        public String PathMaster = "";

        public String EmailPort = "3306";
        public String EmailCharset = "hisorc_ma";        //orc master
        public String EmailUsername = "172.25.1.153";
        public String EmailPassword = "root";
        public String EmailSMTPSecure = "Ekartc2c5";
        public String PathLinfox = "3306";

        public String EmailHost = "hisorc_ba";        // orc backoffice
        public String EmailSender = "172.25.1.153";
        public String FTPServer = "root";
        public String PathZipExtract = "Ekartc2c5";
        public String PathZip = "3306";

        public String databaseDBKFCPO = "bithis";        // orc BIT
        public String hostDBKFCPO = "172.25.1.153";
        public String userDBKFCPO = "root";
        public String passDBKFCPO = "Ekartc2c5";
        public String portDBKFCPO = "3306";

        public String PO005PathInitial = "", PO005PathProcess = "", PO005PathError = "", PO005PathArchive = "", PO005ImportSource="";
        public String PO003PathInitial = "", PO003PathProcess = "", PO003PathError = "", PO003PathArchive = "", PO003ImportSource = "", PO003RECEIPT_SOURCE="", PO003TRANSACTION_TYPE="";
        public String PO004PathInitial = "", PO004PathProcess = "", PO004PathError = "", /*PO004PathArchive = "",*/ PO004ImportSource = "", PO004ZipFileSearch="";
        public String PO008PathInitial = "", PO008PathProcess = "", PO008PathError = "", /*PO004PathArchive = "",*/ PO008ImportSource = "", PO008ZipFileSearch = "";
        public String INV005_2PathInitial = "", INV005_2PathProcess = "", INV005_2PathError = "", INV005_2PathArchive = "", INV005_2ImportSource = "";
    }
}
