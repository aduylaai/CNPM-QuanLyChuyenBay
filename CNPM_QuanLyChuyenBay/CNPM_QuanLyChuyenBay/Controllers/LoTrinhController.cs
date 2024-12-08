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
    public class LoTrinhController : Controller
    {
        DBConnect dbConn = new DBConnect("DUNX\\SQLEXPRESS01", "CNPM_QuanLyBanVeMayBay");
        public LoTrinhController()
        {
            dbConn.openConnect();
        }
        // GET: LoTrinh
        public ActionResult Index()
        {
            List<LoTrinh> dsLoTrinh = new List<LoTrinh>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from LoTrinh");
            while (reader.Read())
            {
                LoTrinh lt = new LoTrinh();
                lt.MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString());
                lt.MaSB_Di = int.Parse(reader["MaSB_Di"].ToString());
                lt.MaSB_Den = int.Parse(reader["MaSB_Den"].ToString());

                dsLoTrinh.Add(lt);
            }
            return View(dsLoTrinh);
        }

        // GET: LoTrinh/Details/5
        public ActionResult Details(int id)
        {
            LoTrinh lt = new LoTrinh();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from LoTrinh where MaLoTrinh =" + id);
            while (reader.Read())
            {
                lt.MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString());
                lt.MaSB_Di = int.Parse(reader["MaSB_Di"].ToString());
                lt.MaSB_Den = int.Parse(reader["MaSB_Den"].ToString());

            }

            return View(lt);
        }

        // GET: LoTrinh/Create
        public ActionResult Create()
        {
            List<SelectListItem> sanBayList = new List<SelectListItem>();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from SanBay");
            while (reader.Read())
            {
                sanBayList.Add(new SelectListItem
                {
                    Value = reader["MaSanBay"].ToString(),
                    Text = reader["TenSanBay"].ToString()
                });
            }
            reader.Close(); 

            ViewBag.SanBayList = sanBayList;

            return View();
        }


        // POST: LoTrinh/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                LoTrinh lt = new LoTrinh();
                lt.MaSB_Di = int.Parse(collection["MaSB_Di"].ToString());
                lt.MaSB_Den = int.Parse(collection["MaSB_Den"].ToString());
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = "insert into LoTrinh values('" + lt.MaSB_Di + "','" + lt.MaSB_Den + "')";
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: LoTrinh/Edit/5
        public ActionResult Edit(int id)
        {
            LoTrinh lt = new LoTrinh();
            SqlDataReader reader = dbConn.ThucThiReader("Select * from LoTrinh where MaLoTrinh =" + id);
            while (reader.Read())
            {
                lt.MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString());
                lt.MaSB_Di = int.Parse(reader["MaSB_Di"].ToString());
                lt.MaSB_Den = int.Parse(reader["MaSB_Den"].ToString());

            }

            return View(lt);
        }

        // POST: LoTrinh/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                LoTrinh lt = new LoTrinh();
                lt.MaSB_Di = int.Parse(collection["MaSB_Di"].ToString());
                lt.MaSB_Den = int.Parse(collection["MaSB_Den"].ToString());

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConn.conn;
                    cmd.CommandText = @"update LoTrinh 
                                        set MaSB_Di = '" + lt.MaSB_Di + "', MaSB_Den = '" + lt.MaSB_Den + "' " +
                                        "where MaLoTrinh = " + id;
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoTrinh/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoTrinh/Delete/5
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
