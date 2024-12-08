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
    public class MayBayController : Controller
    {
        DBConnect dbConn = new DBConnect("DESKTOP-5O90F68", "CNPM_QuanLyBanVeMayBay", "sa", "123");
        public MayBayController()
        {
            dbConn.openConnect();
        }
        // GET: MayBay
        public ActionResult Index()
        {
            List<MayBay> dsMayBay = new List<MayBay>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from MayBay");
            while (reader.Read())
            {
                MayBay mb = new MayBay();
                mb.MaMayBay = int.Parse(reader["MaMayBay"].ToString());
                mb.TenMayBay = reader["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(reader["SucChuaToiDa"].ToString());


                dsMayBay.Add(mb);
            }
            return View(dsMayBay);
        }

        // GET: MayBay/Details/5
        public ActionResult Details(int id)
        {
            MayBay mb = new MayBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from MayBay where MaMayBay =" + id);
            while (reader.Read())
            {
                mb.MaMayBay = int.Parse(reader["MaMayBay"].ToString());
                mb.TenMayBay = reader["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(reader["SucChuaToiDa"].ToString());
            }

            return View(mb);
        }

        // GET: MayBay/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MayBay/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                MayBay mb = new MayBay();
                mb.TenMayBay = collection["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(collection["SucChuaToiDa"].ToString());

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into MayBay values('" + mb.TenMayBay + "','" + mb.SucChuaToiDa + "')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: MayBay/Edit/5
        public ActionResult Edit(int id)
        {
            MayBay mb = new MayBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from MayBay where MaMayBay =" + id);
            while (reader.Read())
            {
                mb.MaMayBay = int.Parse(reader["MaMayBay"].ToString());
                mb.TenMayBay = reader["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(reader["SucChuaToiDa"].ToString());
            }

            return View(mb);
        }

        // POST: MayBay/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                MayBay mb = new MayBay();
                mb.TenMayBay = collection["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(collection["SucChuaToiDa"].ToString());

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update MayBay 
                                        set TenMayBay = '" + mb.TenMayBay + "', SucChuaToiDa = '" + mb.SucChuaToiDa + "' " +
                                        "where MaMayBay = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MayBay/Delete/5
        public ActionResult Delete(int id)
        {
            MayBay mb = new MayBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from MayBay where MaMayBay =" + id);
            while (reader.Read())
            {
                mb.MaMayBay = int.Parse(reader["MaMayBay"].ToString());
                mb.TenMayBay = reader["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(reader["SucChuaToiDa"].ToString());
            }

            return View(mb);
        }

        // POST: MayBay/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                MayBay mb = new MayBay();
                mb.TenMayBay = collection["TenMayBay"].ToString();
                mb.SucChuaToiDa = int.Parse(collection["SucChuaToiDa"].ToString());

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "delete MayBay where MaMayBay = " + id;
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
