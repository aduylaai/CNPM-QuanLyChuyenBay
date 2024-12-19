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
        DBConnect dbConn = new DBConnect(@"DUNX\SQLEXPRESS01", "CNPM_QuanLyBanVeMayBay");
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

                return RedirectToAction("Create", "KhachHang");
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string pTaiKhoan, string pMatKhau)
        {
            if (string.IsNullOrEmpty(pTaiKhoan) || string.IsNullOrEmpty(pMatKhau))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ tài khoản và mật khẩu.";
                return View();
            }

            try
            {
                if (pTaiKhoan == "admin" && pMatKhau == "123")
                {
                    Session["UserName"] = pTaiKhoan;
                    return RedirectToAction("Index", "ChuyenBay");
                }

                string query = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan AND MatKhau = @MatKhau";

                using (SqlConnection conn = new SqlConnection(dbConn.strConnect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenTaiKhoan", pTaiKhoan);
                        cmd.Parameters.AddWithValue("@MatKhau", pMatKhau);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session["UserName"] = pTaiKhoan;

                                if (Session["SearchDeparture"] != null && Session["SearchDestination"] != null)
                                {
                                    return RedirectToAction("KetQuaTimKiem", "DatVe");
                                }
                                else
                                {
                                    return RedirectToAction("TimKiemChuyenBay", "DatVe");
                                }
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
            }

            return View();
        }


        public ActionResult Logout()
        {
            Session["UserName"] = null;

            return RedirectToAction("Login", "TaiKhoan");
        }

    }
}
