using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM_QuanLyChuyenBay.Helpers;
using CNPM_QuanLyChuyenBay.Models;


namespace CNPM_QuanLyChuyenBay.Controllers
{
    public class SanBayController : Controller
    {
        DBConnect dbConn = new DBConnect(".", "CNPM_QuanLyBanVeMayBay");
        public SanBayController()
        {
            dbConn.openConnect();
        }
        // GET: TaiKhoan
        // GET: SanBay
        public ActionResult Index()
        {
            List<SanBay> dsSanBay = new List<SanBay>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from SanBay");
            while (reader.Read())
            {
                SanBay sb = new SanBay();
                sb.MaSanBay = int.Parse(reader["MaSanBay"].ToString());
                sb.TenSanBay = reader["TenSanBay"].ToString();
                sb.TenThanhPho = reader["TenThanhPho"].ToString();
                sb.VietTatSanBay = reader["VietTatSanBay"].ToString();

                dsSanBay.Add(sb);
            }
            return View(dsSanBay);
        }

        // GET: SanBay/Details/5
        public ActionResult Details(int id)
        {
            SanBay sb = new SanBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from SanBay where MaSanBay =" + id);
            while (reader.Read())
            {
                sb.MaSanBay = int.Parse(reader["MaSanBay"].ToString());
                sb.TenSanBay = reader["TenSanBay"].ToString();
                sb.TenThanhPho = reader["TenThanhPho"].ToString();
                sb.VietTatSanBay = reader["VietTatSanBay"].ToString();
            }

            return View(sb);
        }

        // GET: SanBay/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanBay/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                SanBay sb = new SanBay();
                sb.TenSanBay = collection["TenSanBay"].ToString();
                sb.TenThanhPho = collection["TenThanhPho"].ToString();
                sb.VietTatSanBay = collection["VietTatSanBay"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into SanBay values('" + sb.TenSanBay + "','" + sb.TenThanhPho + "','"+sb.VietTatSanBay+"')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: SanBay/Edit/5
        public ActionResult Edit(int id)
        {
            SanBay sb = new SanBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from SanBay where MaSanBay =" + id);
            while (reader.Read())
            {
                sb.MaSanBay = int.Parse(reader["MaSanBay"].ToString());
                sb.TenSanBay= reader["TenSanBay"].ToString();
                sb.TenThanhPho= reader["TenThanhPho"].ToString();
                sb.VietTatSanBay= reader["VietTatSanBay"].ToString();

            }
            return View(sb);
        }

        // POST: SanBay/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                SanBay sb = new SanBay();
                sb.TenSanBay = collection["TenSanBay"].ToString();
                sb.TenThanhPho = collection["TenThanhPho"].ToString();
                sb.VietTatSanBay = collection["VietTatSanBay"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update SanBay 
                                        set TenSanBay = '" + sb.TenSanBay + "', TenThanhPho = '" + sb.TenThanhPho + "',VietTatSanBay='"+sb.VietTatSanBay+"' " +  "where MaSanBay = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SanBay/Delete/5
        public ActionResult Delete(int id)
        {
            SanBay sb = new SanBay();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from SanBay where MaSanBay =" + id);
            while (reader.Read())
            {
                sb.MaSanBay = int.Parse(reader["MaSanBay"].ToString());
                sb.TenSanBay = reader["TenSanBay"].ToString();
                sb.TenThanhPho = reader["TenThanhPho"].ToString();
                sb.VietTatSanBay = reader["VietTatSanBay"].ToString();
            }

            return View(sb);
        }

        // POST: SanBay/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //SanBay sb = new SanBay();
                //sb.TenSanBay = collection["TenSanBay"].ToString();
                //sb.TenThanhPho = collection["TenThanhPho"].ToString();
                //sb.VietTatSanBay = collection["VietTatSanBay"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "delete SanBay where MaSanBay = " + id;
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
