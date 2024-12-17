using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM_QuanLyChuyenBay.Models;
using CNPM_QuanLyChuyenBay.Helpers;
using System.Data.SqlClient;

namespace CNPM_QuanLyChuyenBay.Controllers
{
    public class DatVeController : Controller
    {

        DBConnect dBConn = new DBConnect();


        // GET: DatVe
        public ActionResult Index()
        {
            return View();
        }

        // SB_Di SB_Den NgayDi NgayDen SLKhach HangGhe HHK
        private List<HangHangKhong> LayDSHangHangKhong()
        {
            List<HangHangKhong> dsHHK = new List<HangHangKhong>();

            string cauTruyVan = "select * from HangHangKhong";
            SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan);
            while (reader.Read())
            {
                HangHangKhong HHK = new HangHangKhong();
                HHK.MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString());
                HHK.TenHangHangKhong = reader["TenHangHangKhong"].ToString();
                dsHHK.Add(HHK);
            }
            reader.Close();
            return dsHHK;
        }

        private List<SanBay> LayDSSanBay()
        {
            List<SanBay> dsSB = new List<SanBay>();

            string cauTruyVan = "select * from SanBay";
            SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan);
            while (reader.Read())
            {
                SanBay SB = new SanBay();
                SB.MaSanBay = int.Parse(reader["MaSanBay"].ToString());
                SB.TenSanBay = reader["TenSanBay"].ToString();
                SB.TenThanhPho = reader["TenThanhPho"].ToString();
                SB.VietTatSanBay = reader["VietTatSanBay"].ToString();
                dsSB.Add(SB);
            }
            reader.Close();
            return dsSB;
        }


        public ActionResult TimKiemChuyenBay()
        {
            List<HangHangKhong> dsHHK = LayDSHangHangKhong();
            List<SanBay> dsSB = LayDSSanBay();

            ViewBag.SB = new SelectList(dsSB, "MaSanBay", "TenSanBay");
            ViewBag.HHK = new SelectList(dsHHK, "MaHangHangKhong", "TenHangHangKhong");

            return View();
        }

        [HttpPost]

        public ActionResult TimKiemChuyenBay(int? MaSB_Di, int ? MaSB_Den, int? MaHHK, DateTime? NgayGioDi, int? SLKhach)
        {
            // Tim Chuyen Bay Tra Ve View KetQua
            // Goi y: O view ketqua moi load ds HangGhe len


            return View();
        }

    }
}