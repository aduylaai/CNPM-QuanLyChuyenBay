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
    public class HangGheController : Controller
    {
        DBConnect dbConn = new DBConnect("DESKTOP-5O90F68", "CNPM_QuanLyBanVeMayBay", "sa", "123");
        public HangGheController()
        {
            dbConn.openConnect();
        }
        // GET: HangGhe
        public ActionResult Index()
        {
            List<HangGhe> dsHangGhe = new List<HangGhe>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangGhe");
            while (reader.Read())
            {
                HangGhe hg = new HangGhe();
                hg.MaHangGhe = int.Parse(reader["MaHangGhe"].ToString());
                hg.TenHangGhe = reader["TenHangGhe"].ToString();



                dsHangGhe.Add(hg);
            }
            return View(dsHangGhe);
        }

        // GET: HangGhe/Details/5
        public ActionResult Details(int id)
        {
            HangGhe hg = new HangGhe();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangGhe where MaHangGhe =" + id);
            while (reader.Read())
            {
                hg.MaHangGhe = int.Parse(reader["MaHangGhe"].ToString());
                hg.TenHangGhe = reader["TenHangGhe"].ToString();

            }

            return View(hg);
        }

        // GET: HangGhe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangGhe/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                HangGhe hg = new HangGhe();
                hg.TenHangGhe = collection["TenHangGhe"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into HangGhe values('" + hg.TenHangGhe + "')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: HangGhe/Edit/5
        public ActionResult Edit(int id)
        {
            HangGhe hg = new HangGhe();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangGhe where MaHangGhe =" + id);
            while (reader.Read())
            {
                hg.MaHangGhe = int.Parse(reader["MaHangGhe"].ToString());
                hg.TenHangGhe = reader["TenHangGhe"].ToString();

            }

            return View(hg);
        }

        // POST: HangGhe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                HangGhe hg = new HangGhe();
                hg.TenHangGhe = collection["TenHangGhe"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update HangGhe 
                                        set TenHangGhe = '" + hg.TenHangGhe + "' " +
                                        "where MaHangGhe = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HangGhe/Delete/5
        public ActionResult Delete(int id)
        {
            HangGhe hg = new HangGhe();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from HangGhe where MaHangGhe =" + id);
            while (reader.Read())
            {
                hg.MaHangGhe = int.Parse(reader["MaHangGhe"].ToString());
                hg.TenHangGhe = reader["TenHangGhe"].ToString();

            }

            return View(hg);
        }

        // POST: HangGhe/Delete/5
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
