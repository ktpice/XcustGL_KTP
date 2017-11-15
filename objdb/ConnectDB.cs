//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcustGL_KTP
{
    public class ConnectDB
    {
        //public String databaseDBBIT = "bithis";
        //public String hostDBBIT = "172.25.1.5";
        //public String userDBBIT = "sa";
        //public String passDBBIT = "Orawanhospital1*";
        public String databaseDBBIT = "bithis_demo1";             //bit
        public String hostDBBIT = "172.25.1.153";
        public String userDBBIT = "sa";
        public String passDBBIT = "Orawanhospital1*";
        public String portDBBIT = "3306";

        public String databaseDBBITDemo = "bithis_demo";             //bit demo
        public String hostDBBITDemo = "172.25.1.153";
        public String userDBBITDemo = "sa";
        public String passDBBITDemo = "Orawanhospital1*";
        public String portDBBITDemo = "3306";

        public String databaseDBORCMA = "hisorc_ma";        //orc master
        public String hostDBORCMA = "172.25.1.153";
        public String userDBORCMA = "root";
        public String passDBORCMA = "Ekartc2c5";
        public String portDBORCMA = "3306";

        public String databaseDBORCBA = "hisorc_ba";        // orc backoffice
        public String hostDBORCBA = "172.25.1.153";
        public String userDBORCBA = "root";
        public String passDBORCBA = "Ekartc2c5";
        public String portDBORCBA = "3306";

        public String databaseDBKFCPO = "bithis";        // orc BIT
        public String hostDBKFCPO = "172.25.1.153";
        public String userDBKFCPO = "root";
        public String passDBKFCPO = "Ekartc2c5";
        public String portDBKFCPO = "3306";
        
        public SqlConnection connBIT,  connBITDemo, connKFC;
        //public MySqlConnection connORCMA, connORCBA, connORCBIT;
        public int _rowsAffected = 0;
        private InitC initC;
        public ConnectDB(String host, InitC initc)
        {
            initC = initc;
            if (host == "bit")
            {
                connBIT = new SqlConnection();
                //connMainHIS.ConnectionString = GetConfig(hostName);
                connBIT.ConnectionString = "Server=" + initC.hostDBBIT + ";Database=" + initC.databaseDBBIT + ";Uid=" + initC.userDBBIT + ";Pwd=" + initC.passDBBIT + ";Connection Timeout=300;";
            }            
            else if (host == "bithis")
            {
                connBITDemo = new SqlConnection();
                connBITDemo.ConnectionString = "Server=" + initC.hostDBBITDemo + ";Database=" + initC.databaseDBBITDemo + ";Uid=" + initC.userDBBITDemo + ";Pwd=" + initC.ImportSource + ";Connection Timeout=300;";
            }
            else if (host == "kfc_po")
            {
                connKFC = new SqlConnection();
                //connKFC.ConnectionString = "Server=" + initC.hostDBKFCPO + ";Database=" + initC.databaseDBKFCPO + ";Uid=" + initC.userDBKFCPO + ";Pwd=" + initC.passDBKFCPO + ";port = " + initC.portDBKFCPO + ";Connection Timeout = 300;default command timeout=0; CharSet=utf8;";
                connKFC.ConnectionString = "Server=" + initC.hostDBKFCPO + ";Database=" + initC.databaseDBKFCPO + ";Uid=" + initC.userDBKFCPO + ";Pwd=" + initC.passDBKFCPO + ";";
            }
        }
        public ConnectDB(String hostDB, String databaseDB, String userDB, String passDB)
        {
            connBITDemo = new SqlConnection();
            //connMainHIS.ConnectionString = GetConfig(hostName);
            connBITDemo.ConnectionString = "Server=" + hostDB + ";Database=" + databaseDB + ";Uid=" + userDB + ";Pwd=" + passDB + ";";

        }

        public DataTable selectData(String sql, String host)
        {
            DataTable toReturn = new DataTable();
            if (host == "bit")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBIT;
                SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
                try
                {
                    connBIT.Open();
                    adapMainhis.Fill(toReturn);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connBIT.Close();
                    comMainhis.Dispose();
                    adapMainhis.Dispose();
                }
            }
            
            else if (host == "kfc_po")
            {
                SqlCommand com = new SqlCommand();
                com.CommandText = sql;
                com.Connection = connKFC;
                SqlDataAdapter adap = new SqlDataAdapter(com);
                try
                {
                    connKFC.Open();
                    adap.Fill(toReturn);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connKFC.Close();
                    com.Dispose();
                    adap.Dispose();
                }
            }
            else if (host == "bit_demo")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBITDemo;
                SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
                try
                {
                    connBITDemo.Open();
                    adapMainhis.Fill(toReturn);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connBITDemo.Close();
                    comMainhis.Dispose();
                    adapMainhis.Dispose();
                }
            }
            return toReturn;
        }
        public DataTable selectDataForConvert(String sql, String host)
        {
            DataTable toReturn = new DataTable();
            if (host == "bit")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBIT;
                SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
                try
                {
                    connBIT.Open();
                    adapMainhis.Fill(toReturn);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    var directory = System.IO.Path.GetDirectoryName(path);

                    using (StreamWriter writer = new StreamWriter(directory + "\\error.txt", true))
                    {
                        writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connBIT.Close();
                    comMainhis.Dispose();
                    adapMainhis.Dispose();
                }
            }
            
            else if (host == "bit_demo")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBITDemo;
                SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
                try
                {
                    connBITDemo.Open();
                    adapMainhis.Fill(toReturn);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connBITDemo.Close();
                    comMainhis.Dispose();
                    adapMainhis.Dispose();
                }
            }
            return toReturn;
        }

        public String ExecuteNonQuery(String sql, String host)
        {
            String toReturn = "";
            if (host == "bit")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBIT;
                try
                {
                    connBIT.Open();
                    _rowsAffected = comMainhis.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connBIT.Close();
                    comMainhis.Dispose();
                }
            }            
            else if (host == "kfc_po")
            {
                SqlCommand com = new SqlCommand();
                com.CommandText = sql;
                com.Connection = connKFC;
                try
                {
                    connKFC.Open();
                    _rowsAffected = com.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connKFC.Close();
                    com.Dispose();
                }
            }
            else if (host == "bit_demo")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBITDemo;
                try
                {
                    connBITDemo.Open();
                    _rowsAffected = comMainhis.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connBITDemo.Close();
                    comMainhis.Dispose();
                }
            }
            return toReturn;
        }
        public String ExecuteNonQueryForConvert(String sql, String host)
        {
            String toReturn = "";
            if (host == "bit")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBIT;
                try
                {
                    connBIT.Open();
                    _rowsAffected = comMainhis.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connBIT.Close();
                    comMainhis.Dispose();
                }
            }            
            else if (host == "bit_demo")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBITDemo;
                try
                {
                    connBITDemo.Open();
                    _rowsAffected = comMainhis.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(sql);
                    Console.WriteLine(ex.Message.ToString());

                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    var directory = System.IO.Path.GetDirectoryName(path);

                    using (StreamWriter writer = new StreamWriter(directory + "\\error.txt", true))
                    {
                        writer.WriteLine(sql+Environment.NewLine);
                        writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }

                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connBITDemo.Close();
                    comMainhis.Dispose();
                }
            }
            return toReturn;
        }
        public String ExecuteNonQueryAutoIncrement(String sql, String host)
        {
            String toReturn = "";
            if (host == "bit")
            {
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = connBIT;
                try
                {
                    connBIT.Open();
                    _rowsAffected = comMainhis.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connBIT.Close();
                    comMainhis.Dispose();
                }
            }
            else if (host == "kfc_po")
            {
                SqlCommand com = new SqlCommand();
                com.CommandText = sql;
                com.Connection = connKFC;
                try
                {
                    connKFC.Open();
                    _rowsAffected = com.ExecuteNonQuery();
                    toReturn = _rowsAffected.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ExecuteNonQuery::Error occured.", ex);
                    toReturn = ex.Message;
                }
                finally
                {
                    //_mainConnection.Close();
                    connKFC.Close();
                    com.Dispose();
                }
            }
            return toReturn;
        }
        
    }
}
