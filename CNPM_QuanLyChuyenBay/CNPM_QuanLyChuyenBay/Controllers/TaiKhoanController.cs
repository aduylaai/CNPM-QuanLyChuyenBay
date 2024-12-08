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
    public class TaiKhoanController : Controller
    {
        DBConnect dbConn = new DBConnect("DESKTOP-5O90F68", "CNPM_QuanLyBanVeMayBay", "sa", "123");
        public TaiKhoanController()
        {
            dbConn.openConnect();
        }
        // GET: TaiKhoan
        public ActionResult Index()
        {
            List<TaiKhoan> dsTaiKhoan = new List<TaiKhoan>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from TaiKhoan");
            while (reader.Read())
            {
                TaiKhoan tk = new TaiKhoan();
                tk.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());
                tk.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                tk.MatKhau = reader["MatKhau"].ToString();


                dsTaiKhoan.Add(tk);
            }
            return View(dsTaiKhoan);
        }

        // GET: TaiKhoan/Details/5
        public ActionResult Details(int id)
        {
            TaiKhoan tk = new TaiKhoan();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from TaiKhoan where MaTaiKhoan =" + id);
            while (reader.Read())
            {
                tk.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());
                tk.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                tk.MatKhau = reader["MatKhau"].ToString();
            }

            return View(tk);
        }

        // GET: TaiKhoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                TaiKhoan tk = new TaiKhoan();
                tk.TenTaiKhoan = collection["TenTaiKhoan"].ToString();
                tk.MatKhau = collection["MatKhau"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into TaiKhoan values('"+ tk.TenTaiKhoan+"','"+tk.MatKhau+"')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: TaiKhoan/Edit/5
        public ActionResult Edit(int id)
        {
            TaiKhoan tk = new TaiKhoan();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from TaiKhoan where MaTaiKhoan =" + id);
            while (reader.Read())
            {
                tk.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());
                tk.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                tk.MatKhau = reader["MatKhau"].ToString();
            }

            return View(tk);
        }

        // POST: TaiKhoan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                TaiKhoan tk = new TaiKhoan();
                tk.TenTaiKhoan = collection["TenTaiKhoan"].ToString();
                tk.MatKhau = collection["MatKhau"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update TaiKhoan 
                                        set TenTaiKhoan = '"+ tk.TenTaiKhoan +"', MatKhau = '"+ tk.MatKhau+"' " +
                                        "where MaTaiKhoan = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TaiKhoan/Delete/5
        public ActionResult Delete(int id)
        {
            TaiKhoan tk = new TaiKhoan();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from TaiKhoan where MaTaiKhoan =" + id);
            while (reader.Read())
            {
                tk.MaTaiKhoan = int.Parse(reader["MaTaiKhoan"].ToString());
                tk.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                tk.MatKhau = reader["MatKhau"].ToString();
            }

            return View(tk);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TaiKhoan tk = new TaiKhoan();
                tk.TenTaiKhoan = collection["TenTaiKhoan"].ToString();
                tk.MatKhau = collection["MatKhau"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "delete TaiKhoan where MaTaiKhoan = " + id;
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
