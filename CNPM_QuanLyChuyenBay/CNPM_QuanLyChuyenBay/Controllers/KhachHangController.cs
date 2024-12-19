using CNPM_QuanLyChuyenBay.Helpers;
using CNPM_QuanLyChuyenBay.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPM_QuanLyChuyenBay.Controllers
{
    public class KhachHangController : Controller
    {
        DBConnect dbConn = new DBConnect(".", "CNPM_QuanLyBanVeMayBay");
        // GET: KhachHang
        public KhachHangController()
        {
            dbConn.openConnect();
        }
        public ActionResult Index()
        {
            List<KhachHang> dsKhachHang = new List<KhachHang>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from KhachHang");
            while (reader.Read())
            {
                KhachHang kh = new KhachHang();
                kh.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
                kh.HoTen = reader["HoTen"].ToString();
                kh.DiaChi = reader["DiaChi"].ToString();
                kh.Email = reader["Email"].ToString();
                kh.NgaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                kh.SoDienThoai = reader["SoDienThoai"].ToString();
                kh.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());

                dsKhachHang.Add(kh);
            }
            return View(dsKhachHang);
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.HoTen = collection["HoTen"].ToString();
                kh.DiaChi = collection["DiaChi"].ToString();
                kh.Email = collection["Email"].ToString();

                // Kiểm tra và chuyển đổi ngày sinh an toàn
                if (!DateTime.TryParse(collection["NgaySinh"], out DateTime ngaySinh))
                {
                    ModelState.AddModelError("NgaySinh", "Định dạng ngày không hợp lệ.");
                    return View();
                }
                kh.NgaySinh = ngaySinh;

                // Chuyển đổi số an toàn
                if (!int.TryParse(collection["MaTaiKhoan"], out int maTaiKhoan))
                {
                    ModelState.AddModelError("MaTaiKhoan", "Mã tài khoản phải là số.");
                    return View();
                }
                kh.MaTaiKhoan = maTaiKhoan;

                kh.SoDienThoai = collection["SoDienThoai"].ToString();

                // Sử dụng truy vấn tham số hóa
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"
                INSERT INTO KhachHang (HoTen, DiaChi, Email, NgaySinh, SoDienThoai, MaTaiKhoan) 
                VALUES (@HoTen, @DiaChi, @Email, @NgaySinh, @SoDienThoai, @MaTaiKhoan)";

                    // Gán giá trị tham số
                    cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                    cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                    cmd.Parameters.AddWithValue("@Email", kh.Email);
                    cmd.Parameters.AddWithValue("@NgaySinh", kh.NgaySinh);
                    cmd.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", kh.MaTaiKhoan);

                    // Mở kết nối nếu chưa mở
                    if (dbConn.conn.State == System.Data.ConnectionState.Closed)
                        dbConn.conn.Open();

                    // Thực thi lệnh
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("TimKiemChuyenBay", "DatVe");
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần) và hiển thị lại View
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }


        // GET: KhachHang/Edit/5
        public ActionResult Edit()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string currentUserName = Session["UserName"].ToString();
            KhachHang kh = new KhachHang();

            // Prepare the query to get the logged-in customer's information
            string query = @"
                    SELECT KH.* 
                    FROM KhachHang KH 
                    INNER JOIN TaiKhoan TK ON KH.MaTaiKhoan = TK.MaTaiKhoan 
                    WHERE TK.TenTaiKhoan = @UserName";

            using (SqlCommand cmd = new SqlCommand(query, dbConn.conn))
            {
                cmd.Parameters.AddWithValue("@UserName", currentUserName);

                if (dbConn.conn.State == System.Data.ConnectionState.Closed)
                {
                    dbConn.conn.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kh.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
                        kh.HoTen = reader["HoTen"].ToString();
                        kh.DiaChi = reader["DiaChi"].ToString();
                        kh.Email = reader["Email"].ToString();
                        kh.NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString());
                        kh.SoDienThoai = reader["SoDienThoai"].ToString();
                        kh.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());
                    }
                }
            }

            return View(kh);
        }

        // POST: KhachHang/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                string currentUserName = Session["UserName"].ToString();
                KhachHang kh = new KhachHang();

                // Retrieve and validate form inputs
                kh.HoTen = collection["HoTen"].ToString();
                kh.DiaChi = collection["DiaChi"].ToString();
                kh.Email = collection["Email"].ToString();

                if (!DateTime.TryParse(collection["NgaySinh"], out DateTime ngaySinh))
                {
                    ModelState.AddModelError("NgaySinh", "Định dạng ngày không hợp lệ.");
                    return View(kh);
                }
                kh.NgaySinh = ngaySinh;

                kh.SoDienThoai = collection["SoDienThoai"].ToString();

                // Fetch the customer's MaTaiKhoan based on the logged-in user
                string selectQuery = "SELECT MaTaiKhoan FROM TaiKhoan WHERE TenTaiKhoan = @UserName";

                int maTaiKhoan;
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, dbConn.conn))
                {
                    selectCmd.Parameters.AddWithValue("@UserName", currentUserName);

                    if (dbConn.conn.State == System.Data.ConnectionState.Closed)
                    {
                        dbConn.conn.Open();
                    }

                    object result = selectCmd.ExecuteScalar();
                    if (result == null)
                    {
                        return HttpNotFound("Tài khoản không tồn tại.");
                    }
                    maTaiKhoan = Convert.ToInt32(result);
                }

                string updateQuery = @"
                        UPDATE KhachHang 
                        SET HoTen = @HoTen, 
                            DiaChi = @DiaChi, 
                            Email = @Email, 
                            NgaySinh = @NgaySinh, 
                            SoDienThoai = @SoDienThoai
                        WHERE MaTaiKhoan = @MaTaiKhoan";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, dbConn.conn))
                {
                    updateCmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                    updateCmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                    updateCmd.Parameters.AddWithValue("@Email", kh.Email);
                    updateCmd.Parameters.AddWithValue("@NgaySinh", kh.NgaySinh);
                    updateCmd.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                    updateCmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                    updateCmd.ExecuteNonQuery();
                }

                return RedirectToAction("TimKiemChuyenBay", "DatVe");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }


        // GET: KhachHang/Delete/5
        public ActionResult Delete(int id)
        {
            KhachHang kh = new KhachHang();
            SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM KhachHang WHERE MaKhachHang = " + id);
            while (reader.Read())
            {
                kh.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
                kh.HoTen = reader["HoTen"].ToString();
                kh.DiaChi = reader["DiaChi"].ToString();
                kh.Email = reader["Email"].ToString();
                kh.SoDienThoai = reader["SoDienThoai"].ToString();
                kh.NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString());
            }

            return View(kh);
        }


        // POST: KhachHang/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "DELETE FROM KhachHang WHERE MaKhachHang = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
