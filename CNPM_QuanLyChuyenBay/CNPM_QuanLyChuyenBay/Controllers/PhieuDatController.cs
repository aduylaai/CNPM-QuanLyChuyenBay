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
    public class PhieuDatController : Controller
    {
        DBConnect dbConn = new DBConnect(".", "CNPM_QuanLyBanVeMayBay");
        public PhieuDatController()
        {
            dbConn.openConnect();
        }
        // GET: PhieuDat
        public ActionResult Index()
        {
            List<PhieuDat> phieuDats = new List<PhieuDat>();

            SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM PhieuDat");
            while (reader.Read())
            {
                PhieuDat pd = new PhieuDat
                {
                    // Kiểm tra nếu giá trị không phải là NULL và chuyển đổi an toàn
                    MaPhieuDat = reader["MaPhieuDat"] != DBNull.Value ? int.Parse(reader["MaPhieuDat"].ToString()) : 0,
                    MaKhachHang = reader["MaKhachHang"] != DBNull.Value ? int.Parse(reader["MaKhachHang"].ToString()) : 0,
                    NgayDat = reader["NgayDat"] != DBNull.Value ? DateTime.Parse(reader["NgayDat"].ToString()) : DateTime.MinValue,
                    MaBooking = reader["MaBooking"] != DBNull.Value ? int.Parse(reader["MaBooking"].ToString()) : 0
                };

                phieuDats.Add(pd);
            }

            return View(phieuDats);
        }


        // GET: PhieuDat/Details/5
        public ActionResult Details(int id)
        {
            PhieuDat pd = new PhieuDat();

            SqlDataReader reader = dbConn.ThucThiReader("Select * from PhieuDat where MaPhieuDat =" + id);

            // Đọc dữ liệu từ SqlDataReader và gán vào đối tượng PhieuDat
            while (reader.Read())
            {
                // Kiểm tra NULL và chuyển đổi an toàn
                pd.MaPhieuDat = reader["MaPhieuDat"] != DBNull.Value ? int.Parse(reader["MaPhieuDat"].ToString()) : 0;
                pd.MaKhachHang = reader["MaKhachHang"] != DBNull.Value ? int.Parse(reader["MaKhachHang"].ToString()) : 0;

                // Kiểm tra NULL và chuyển đổi an toàn cho NgayDat
                pd.NgayDat = reader["NgayDat"] != DBNull.Value ? DateTime.Parse(reader["NgayDat"].ToString()) : DateTime.MinValue;

                pd.MaBooking = reader["MaBooking"] != DBNull.Value ? int.Parse(reader["MaBooking"].ToString()) : 0;
            }


            // Đảm bảo đóng kết nối sau khi đọc dữ liệu xong
            reader.Close();

            // Trả về View với đối tượng PhieuDat
            return View(pd);
        }


        // GET: PhieuDat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhieuDat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PhieuDat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PhieuDat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PhieuDat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhieuDat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
