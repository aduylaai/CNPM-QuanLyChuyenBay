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
    public class HangKhachController : Controller
    {
        DBConnect dbConn = new DBConnect(".", "CNPM_QuanLyBanVeMayBay");
        public HangKhachController()
        {
            dbConn.openConnect();
        }
        public ActionResult Index()
        {
            List<HanhKhach> hanhKhachs = new List<HanhKhach>();

            SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM HanhKhach");
            while (reader.Read())
            {
                HanhKhach hk = new HanhKhach
                {
                    MaHanhKhach = int.Parse(reader["MaHanhKhach"].ToString()),
                    HoTen = reader["HoTen"].ToString(),
                    DiaChi = reader["DiaChi"].ToString(),
                    GioiTinh = reader["GioiTinh"].ToString(),
                    QuocTich = reader["QuocTich"].ToString(),
                    NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString()),
                    SoDienThoai = reader["SoDienThoai"].ToString(),
                    Email = reader["Email"].ToString(),
                    CCCD_Passport = reader["CCCD_Passport"].ToString(),
                    MaKhachHang = int.Parse(reader["MaKhachHang"].ToString())
                };
                hanhKhachs.Add(hk);
            }

            return View(hanhKhachs);
        }



        // GET: HangKhach/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HangKhach/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangKhach/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                HanhKhach hk = new HanhKhach
                {
                    HoTen = collection["HoTen"].ToString(),
                    DiaChi = collection["DiaChi"].ToString(),
                    GioiTinh = collection["GioiTinh"].ToString(),
                    QuocTich = collection["QuocTich"].ToString(),
                    NgaySinh = DateTime.Parse(collection["NgaySinh"]),
                    SoDienThoai = collection["SoDienThoai"].ToString(),
                    Email = collection["Email"].ToString(),
                    CCCD_Passport = collection["CCCD_Passport"].ToString(),
                    MaKhachHang = int.Parse(collection["MaKhachHang"])
                };

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"
                INSERT INTO HanhKhach (HoTen, DiaChi, GioiTinh, QuocTich, NgaySinh, SoDienThoai, Email, CCCD_Passport, MaKhachHang)
                VALUES (@HoTen, @DiaChi, @GioiTinh, @QuocTich, @NgaySinh, @SoDienThoai, @Email, @CCCD_Passport, @MaKhachHang)";
                    cmd.Parameters.AddWithValue("@HoTen", hk.HoTen);
                    cmd.Parameters.AddWithValue("@DiaChi", hk.DiaChi);
                    cmd.Parameters.AddWithValue("@GioiTinh", hk.GioiTinh);
                    cmd.Parameters.AddWithValue("@QuocTich", hk.QuocTich);
                    cmd.Parameters.AddWithValue("@NgaySinh", hk.NgaySinh);
                    cmd.Parameters.AddWithValue("@SoDienThoai", hk.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", hk.Email);
                    cmd.Parameters.AddWithValue("@CCCD_Passport", hk.CCCD_Passport);
                    cmd.Parameters.AddWithValue("@MaKhachHang", hk.MaKhachHang);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: HangKhach/Edit/5
        public ActionResult Edit(int id)
        {
            HanhKhach hk = new HanhKhach();

            SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM HanhKhach WHERE MaHanhKhach = " + id);
            while (reader.Read())
            {
                hk.MaHanhKhach = int.Parse(reader["MaHanhKhach"].ToString());
                hk.HoTen = reader["HoTen"].ToString();
                hk.DiaChi = reader["DiaChi"].ToString();
                hk.GioiTinh = reader["GioiTinh"].ToString();
                hk.QuocTich = reader["QuocTich"].ToString();
                hk.NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString());
                hk.SoDienThoai = reader["SoDienThoai"].ToString();
                hk.Email = reader["Email"].ToString();
                hk.CCCD_Passport = reader["CCCD_Passport"].ToString();
                hk.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
            }

            return View(hk);
        }



        // POST: HangKhach/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                HanhKhach hk = new HanhKhach
                {
                    HoTen = collection["HoTen"].ToString(),
                    DiaChi = collection["DiaChi"].ToString(),
                    GioiTinh = collection["GioiTinh"].ToString(),
                    QuocTich = collection["QuocTich"].ToString(),
                    NgaySinh = DateTime.Parse(collection["NgaySinh"]),
                    SoDienThoai = collection["SoDienThoai"].ToString(),
                    Email = collection["Email"].ToString(),
                    CCCD_Passport = collection["CCCD_Passport"].ToString(),
                    MaKhachHang = int.Parse(collection["MaKhachHang"])
                };

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"
                UPDATE HanhKhach 
                SET HoTen = @HoTen, DiaChi = @DiaChi, GioiTinh = @GioiTinh, QuocTich = @QuocTich, 
                    NgaySinh = @NgaySinh, SoDienThoai = @SoDienThoai, Email = @Email, 
                    CCCD_Passport = @CCCD_Passport, MaKhachHang = @MaKhachHang
                WHERE MaHanhKhach = @MaHanhKhach";
                    cmd.Parameters.AddWithValue("@HoTen", hk.HoTen);
                    cmd.Parameters.AddWithValue("@DiaChi", hk.DiaChi);
                    cmd.Parameters.AddWithValue("@GioiTinh", hk.GioiTinh);
                    cmd.Parameters.AddWithValue("@QuocTich", hk.QuocTich);
                    cmd.Parameters.AddWithValue("@NgaySinh", hk.NgaySinh);
                    cmd.Parameters.AddWithValue("@SoDienThoai", hk.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", hk.Email);
                    cmd.Parameters.AddWithValue("@CCCD_Passport", hk.CCCD_Passport);
                    cmd.Parameters.AddWithValue("@MaKhachHang", hk.MaKhachHang);
                    cmd.Parameters.AddWithValue("@MaHanhKhach", id);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: HangKhach/Delete/5
        public ActionResult Delete(int id)
        {
            HanhKhach hk = new HanhKhach();

            SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM HanhKhach WHERE MaHanhKhach = " + id);
            while (reader.Read())
            {
                hk.MaHanhKhach = int.Parse(reader["MaHanhKhach"].ToString());
                hk.HoTen = reader["HoTen"].ToString();
                hk.DiaChi = reader["DiaChi"].ToString();
                hk.GioiTinh = reader["GioiTinh"].ToString();
                hk.QuocTich = reader["QuocTich"].ToString();
                hk.NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString());
                hk.SoDienThoai = reader["SoDienThoai"].ToString();
                hk.Email = reader["Email"].ToString();
                hk.CCCD_Passport = reader["CCCD_Passport"].ToString();
                hk.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
            }

            return View(hk);
        }



        // POST: HangKhach/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "DELETE FROM HanhKhach WHERE MaHanhKhach = " + id;
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
