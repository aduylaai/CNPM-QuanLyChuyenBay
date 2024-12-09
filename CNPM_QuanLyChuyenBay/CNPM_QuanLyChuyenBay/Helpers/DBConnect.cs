using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Helpers
{
    public class DBConnect
    {

        private SqlConnection _conn;
        private string _strConnect, _strServerName, _strDBName, _strUserID, _strPassword;
        public SqlConnection conn
        {
            get { return _conn; }
            set { _conn = value; }
        }
        public string strConnect
        {
            get { return _strConnect; }
            set { _strConnect = value; }
        }
        public string strServerName
        {
            get { return _strServerName; }
            set { _strServerName = value; }
        }
        public string strDBName
        {
            get { return _strDBName; }
            set { _strDBName = value; }
        }
        public string strUserID
        {
            get { return _strUserID; }
            set { _strUserID = value; }
        }
        public string strPassword
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }

        public DBConnect(string pServerName, string pDBName)
        { //Dùng với quyền của của Windows
            strServerName = pServerName; strDBName = pDBName;

            strConnect = @"Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";Integrated Security=true";
            conn = new SqlConnection(strConnect); //Khởi tạo đối tượng kết nối đến CSDL
        }
        public DBConnect(string pServerName, string pDBName, string pUserID, string pPassword)
        { //Dùng với quyền của SQL Server
            strServerName = pServerName; strDBName = pDBName; strUserID = pUserID; strPassword = pPassword;
            strConnect = @"Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";User ID=" + strUserID + ";Password=" + strPassword;
            conn = new SqlConnection(strConnect); //Khởi tạo đối tượng kết nối đến CSDL
        }

        public DBConnect()
        {

            strServerName = "."; strDBName = "QuanLyBanVeMayBay";

            strConnect = @"Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";Integrated Security=true";
            conn = new SqlConnection(strConnect); //Khởi tạo đối tượng kết nối đến CSDL
        }
        public void openConnect()
        { //Mở kết nối
            if (conn.State == ConnectionState.Closed) conn.Open();
        }
        public void closeConnect()
        { //Đóng kết nối
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        public void disposeConnect()
        { //Hủy đối tượng kết nối
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose(); //Giải phóng vùng nhớ đã cấp phát cho conn
        }
        public void updateToDataBase(string strSQL)
        {
            openConnect(); //Mở kết nối
            SqlCommand cmd = new SqlCommand(); cmd.Connection = conn;
            cmd.CommandText = strSQL;
            cmd.ExecuteNonQuery();
            closeConnect();
        }
        public int getCount(string strSQL)
        {
            openConnect(); //Mở kết nối
            SqlCommand cmd = new SqlCommand(); cmd.Connection = conn;
            cmd.CommandText = strSQL; //Câu truy vấn đưa vào
            int count = (int)cmd.ExecuteScalar(); //Thực thi closeConnect(); //Đóng kết nối
            return count;

        }
        public bool checkExist(string tableName, string fieldName, string fieldValue)
        {
            try
            {
                string strSQL = "SELECT COUNT(*) FROM " + tableName + " WHERE " + fieldName + "='"
           + fieldValue + "'";
                return getCount(strSQL) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }
           ;
        }

        public bool ktraTaiKhoan(string tableName, string taiKhoan, string matKhau)
        {
            try
            {
                string strSQL = "SELECT COUNT(*) FROM " + tableName + " WHERE TenTaiKhoan ='"
           + taiKhoan + "' AND MatKhau = '" + matKhau + "'";
                return getCount(strSQL) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }
          ;
        }

        public SqlDataReader ThucThiReader(string cauTruyVan)
        {
            //using (SqlCommand cmd = new SqlCommand(cauTruyVan, conn))
            //{
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    return reader;
            //}
            try
            {
                // Đảm bảo kết nối được mở trước khi thực thi
                openConnect();

                using (SqlCommand cmd = new SqlCommand(cauTruyVan, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
            catch (Exception ex)
            {
                closeConnect();
                throw new Exception("Lỗi khi thực thi câu truy vấn: " + ex.Message);
            }
        }


    }
}