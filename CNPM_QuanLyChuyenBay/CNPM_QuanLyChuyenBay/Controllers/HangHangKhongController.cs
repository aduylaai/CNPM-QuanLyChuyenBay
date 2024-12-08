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
    public class HangHangKhongController : Controller
    {
        DBConnect dbConn = new DBConnect("DESKTOP-5O90F68", "CNPM_QuanLyBanVeMayBay", "sa", "123");
        public HangHangKhongController()
        {
            dbConn.openConnect();
        }
        // GET: HangHangKhong
        public ActionResult Index()
        {
            List<HangHangKhong> dsHangHangKhong = new List<HangHangKhong>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangHangKhong");
            while (reader.Read())
            {
                HangHangKhong hhk = new HangHangKhong();
                hhk.MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString());
                hhk.TenHangHangKhong = reader["TenHangHangKhong"].ToString();



                dsHangHangKhong.Add(hhk);
            }
            return View(dsHangHangKhong);
        }

        // GET: HangHangKhong/Details/5
        public ActionResult Details(int id)
        {
            HangHangKhong hhk = new HangHangKhong();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangHangKhong where MaHangHangKhong =" + id);
            while (reader.Read())
            {
                hhk.MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString());
                hhk.TenHangHangKhong = reader["TenHangHangKhong"].ToString();

            }

            return View(hhk);
        }

        // GET: HangHangKhong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangHangKhong/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               HangHangKhong hhk = new HangHangKhong();
               hhk.TenHangHangKhong= collection["TenHangHangKhong"].ToString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into HangHangKhong values('" + hhk.TenHangHangKhong + "')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: HangHangKhong/Edit/5
        public ActionResult Edit(int id)
        {
            HangHangKhong hhk = new HangHangKhong();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangHangKhong where MaHangHangKhong =" + id);
            while (reader.Read())
            {
                hhk.MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString());
                hhk.TenHangHangKhong = reader["TenHangHangKhong"].ToString();

            }

            return View(hhk);
        }

        // POST: HangHangKhong/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                HangHangKhong hhk = new HangHangKhong();
                hhk.TenHangHangKhong = collection["TenHangHangKhong"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update HangHangKhong 
                                        set TenHangHangKhong = '" + hhk.TenHangHangKhong + "' " +
                                        "where MaHangHangKhong = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HangHangKhong/Delete/5
        public ActionResult Delete(int id)
        {
            HangHangKhong hhk = new HangHangKhong();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangHangKhong where MaHangHangKhong =" + id);
            while (reader.Read())
            {
                hhk.MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString());
                hhk.TenHangHangKhong = reader["TenHangHangKhong"].ToString();

            }

            return View(hhk);
        }

        // POST: HangHangKhong/Delete/5
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
