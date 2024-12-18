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

        DBConnect dBConn = new DBConnect(@"DUNX\SQLEXPRESS01", "CNPM_QuanLyBanVeMayBay");


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

        private List<HangGhe> LayDSHanGhe()
        {
            List<HangGhe> dsHG = new List<HangGhe>();

            string cauTruyVan = "Select * from HangGhe";
            SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan);
            while (reader.Read())
            {
                HangGhe hg = new HangGhe();
                hg.MaHangGhe = int.Parse(reader["MaHangGhe"].ToString());
                hg.TenHangGhe = reader["TenHangGhe"].ToString();

                dsHG.Add(hg);
            }
            reader.Close();
            return dsHG;
        }
        public ActionResult TimKiemChuyenBay()
        {
            List<HangHangKhong> dsHHK = LayDSHangHangKhong();
            List<SanBay> dsSB = LayDSSanBay();
            List<HangGhe> dsHG = LayDSHanGhe();

            ViewBag.HG = new SelectList(dsHG, "MaHangGhe", "TenHangGhe");
            ViewBag.SB = new SelectList(dsSB, "MaSanBay", "TenSanBay");
            ViewBag.HHK = new SelectList(dsHHK, "MaHangHangKhong", "TenHangHangKhong");

            return View();
        }

        [HttpPost]

        public ActionResult TimKiemChuyenBay(int? MaSB_Di, int? MaSB_Den, int? MaHHK, DateTime? NgayGioDi, int? SLKhach, int MaHG)
        {
            // Tim Chuyen Bay Tra Ve View KetQua
            // Goi y: O view ketqua moi load ds HangGhe len

            ViewBag.SLKhach = SLKhach;

            // Danh sách kết quả tìm kiếm
            List<KetQuaTimKiem> result = new List<KetQuaTimKiem>();

            // Mở kết nối tới cơ sở dữ liệu
            dBConn.openConnect();

            try
            {
                // Tạo câu truy vấn
                string cauTruyVan = @"
            SELECT 
                cb.MaChuyenBay, 
                hhk.TenHangHangKhong, 
                sb_di.TenSanBay AS DiemDi, 
                sb_den.TenSanBay AS DiemDen, 
                cb.NgayGioDi, 
                cb.NgayGioDen,
                cb.SLGhePhoThong,
                cb.SLGheThuongGia,
                cb.GiaBay
            FROM 
                ChuyenBay cb
            JOIN 
                LoTrinh lt ON lt.MaLoTrinh = cb.MaLoTrinh
            JOIN 
                HangHangKhong hhk ON hhk.MaHangHangKhong = cb.MaHangHangKhong
            JOIN 
                SanBay sb_di ON sb_di.MaSanBay = lt.MaSB_Di
            JOIN 
                SanBay sb_den ON sb_den.MaSanBay = lt.MaSB_Den
            WHERE
                (@sanBayDiID IS NULL OR sb_di.MaSanBay = @sanBayDiID) AND
                (@sanBayDenID IS NULL OR sb_den.MaSanBay = @sanBayDenID) AND
                (@maHHK IS NULL OR hhk.MaHangHangKhong = @maHHK) AND
                (@ngayGioDi IS NULL OR CAST(cb.NgayGioDi AS DATE) = CAST(@ngayGioDi AS DATE)) AND
                (@slKhach IS NULL OR cb.SLGhePhoThong >= @slKhach OR cb.SLGheThuongGia >= @slKhach)";

                // Tạo các tham số cho truy vấn
                SqlParameter[] parameters = {
            new SqlParameter("@sanBayDiID", MaSB_Di.HasValue ? (object)MaSB_Di.Value : DBNull.Value),
            new SqlParameter("@sanBayDenID", MaSB_Den.HasValue ? (object)MaSB_Den.Value : DBNull.Value),
            new SqlParameter("@maHHK", MaHHK.HasValue ? (object)MaHHK.Value : DBNull.Value),
            new SqlParameter("@ngayGioDi", NgayGioDi.HasValue ? (object)NgayGioDi.Value : DBNull.Value),
            new SqlParameter("@slKhach", SLKhach.HasValue ? (object)SLKhach.Value : DBNull.Value)
        };

                // Thực thi truy vấn và đọc kết quả
                using (SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan, parameters))
                {
                    while (reader.Read())
                    {
                        // Tạo đối tượng kết quả và thêm vào danh sách
                        KetQuaTimKiem item = new KetQuaTimKiem
                        {
                            MaChuyenBay = reader["MaChuyenBay"] != DBNull.Value ? Convert.ToInt32(reader["MaChuyenBay"]) : 0,
                            TenHangHangKhong = reader["TenHangHangKhong"]?.ToString(),
                            TenSB_Di = reader["DiemDi"]?.ToString(),
                            TenSB_Den = reader["DiemDen"]?.ToString(),
                            NgayGioDi = reader["NgayGioDi"] != DBNull.Value ? Convert.ToDateTime(reader["NgayGioDi"]) : DateTime.MinValue,
                            NgayGioDen = reader["NgayGioDen"] != DBNull.Value ? Convert.ToDateTime(reader["NgayGioDen"]) : DateTime.MinValue,
                            SLGhePhoThong = reader["SLGhePhoThong"] != DBNull.Value ? Convert.ToInt32(reader["SLGhePhoThong"]) : 0,
                            SLGheThuongGia = reader["SLGheThuongGia"] != DBNull.Value ? Convert.ToInt32(reader["SLGheThuongGia"]) : 0,
                            GiaBay = reader["GiaBay"] != DBNull.Value ? Convert.ToDecimal(reader["GiaBay"]) : 0
                        };
                        result.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                dBConn.closeConnect();
            }

            TempData["HangGhe"] = MaHG;
            // Trả về View và truyền danh sách kết quả
            return View("KetQuaTimKiem", result);
        }

        private KetQuaTimKiem LayChuyenBay(int idChuyenBay)
        {
            KetQuaTimKiem kq = new KetQuaTimKiem();

            dBConn.openConnect();

            try
            {
                // Tạo câu truy vấn
                string cauTruyVan = @"
            SELECT 
                cb.MaChuyenBay, 
                hhk.TenHangHangKhong, 
                sb_di.TenSanBay AS DiemDi, 
                sb_den.TenSanBay AS DiemDen, 
                cb.NgayGioDi, 
                cb.NgayGioDen,
                cb.SLGhePhoThong,
                cb.SLGheThuongGia,
                cb.GiaBay
            FROM 
                ChuyenBay cb
            JOIN 
                LoTrinh lt ON lt.MaLoTrinh = cb.MaLoTrinh
            JOIN 
                HangHangKhong hhk ON hhk.MaHangHangKhong = cb.MaHangHangKhong
            JOIN 
                SanBay sb_di ON sb_di.MaSanBay = lt.MaSB_Di
            JOIN 
                SanBay sb_den ON sb_den.MaSanBay = lt.MaSB_Den
            WHERE
                cb.MaChuyenBay = " + idChuyenBay;

                // Thực thi truy vấn và đọc kết quả
                using (SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan))
                {
                    while (reader.Read())
                    {
                        // Tạo đối tượng kết quả và thêm vào danh sách
                        KetQuaTimKiem item = new KetQuaTimKiem
                        {
                            MaChuyenBay = reader["MaChuyenBay"] != DBNull.Value ? Convert.ToInt32(reader["MaChuyenBay"]) : 0,
                            TenHangHangKhong = reader["TenHangHangKhong"]?.ToString(),
                            TenSB_Di = reader["DiemDi"]?.ToString(),
                            TenSB_Den = reader["DiemDen"]?.ToString(),
                            NgayGioDi = reader["NgayGioDi"] != DBNull.Value ? Convert.ToDateTime(reader["NgayGioDi"]) : DateTime.MinValue,
                            NgayGioDen = reader["NgayGioDen"] != DBNull.Value ? Convert.ToDateTime(reader["NgayGioDen"]) : DateTime.MinValue,
                            SLGhePhoThong = reader["SLGhePhoThong"] != DBNull.Value ? Convert.ToInt32(reader["SLGhePhoThong"]) : 0,
                            SLGheThuongGia = reader["SLGheThuongGia"] != DBNull.Value ? Convert.ToInt32(reader["SLGheThuongGia"]) : 0,
                            GiaBay = reader["GiaBay"] != DBNull.Value ? Convert.ToDecimal(reader["GiaBay"]) : 0
                        };
                        kq = item;
                    }

                }
                return kq;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public ActionResult DienThongTinKhach(int id, int? SLKhach)
        {
            KetQuaTimKiem ThongTinChuyenBay = LayChuyenBay(id);

            ViewBag.SLKhach = SLKhach;
            TempData["TTChuyenBay"] = ThongTinChuyenBay;
            //Load thong tin chuyen bay len
            //Dien thong tin khach hang theo so luong hanh khach

            return View();
        }

        private PHanhKhach GetHanhKhach(string cccd)
        {
            PHanhKhach tmp = new PHanhKhach();
            string cauTruyVan = "select * from HanhKhach where CCCD_Passport = '" + cccd + "'";
            SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan);
            while (reader.Read())
            {
                tmp.MaHanhKhach = int.Parse(reader["MaHanhKhach"].ToString());
                tmp.HoTen = reader["HoTen"].ToString();
                tmp.QuocTich = reader["QuocTich"].ToString();
                tmp.NgaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                tmp.CCCD_Passport = reader["CCCD_Passport"].ToString();
                tmp.GioiTinh = reader["GioiTinh"].ToString();
            }

            return tmp;
        }

        [HttpPost]
        public JsonResult KiemTraCCCD(string cccd)
        {
            PHanhKhach hanhKhach = GetHanhKhach(cccd);
            if (hanhKhach != null)
            {
                return Json(new
                {
                    status = "found",
                    data = new
                    {
                        hanhKhach.HoTen,
                        hanhKhach.GioiTinh,
                        hanhKhach.QuocTich,
                        hanhKhach.NgaySinh
                    }
                });
            }
            else
            {
                return Json(new { status = "not_found" });
            }
        }

        [HttpPost]
        public ActionResult XacNhanTTKhach(List<PHanhKhach> HanhKhach, int idChuyenBay)
        {
            //TODO: Hiện thông tin chuyến bay + Danh Sách khách để Đặt ==> Hỏi thanh toán luôn ko để add vào
            KetQuaTimKiem ThongTinChuyenBay = LayChuyenBay(idChuyenBay);

            TempData["TTChuyenBay"] = ThongTinChuyenBay;
            TempData["DanhSachHanhKhach"] = HanhKhach;
            return View(HanhKhach);
        }

        [HttpPost]
        public ActionResult XemTruocThanhToan(int idChuyenBay)
        {
            KetQuaTimKiem thongTinChuyenBay = LayChuyenBay(idChuyenBay);
            List<PHanhKhach> hanhKhach = TempData["DanhSachHanhKhach"] as List<PHanhKhach>;
            int maHG = int.Parse(TempData["HangGhe"].ToString());
            //Tao 1 obj HoaDon ==> TinhTongTien + Add MaPhieuDat + Add NgayHienTai ==> Show ra man hinh Tong tien

            //Lay gia hang ghe
            string cauTruyVan = @"select Gia
                                  from GiaHangGhe ghg
                                  join HangHangKhong hhk on ghg.MaHHK = hhk.MaHangHangKhong
                                  where MaHangGhe = "+maHG+" and hhk.TenHangHangKhong = N'"+thongTinChuyenBay.TenHangHangKhong+"'";
            SqlDataReader reader = dBConn.ThucThiReader(cauTruyVan);
            decimal GiaHangGhe = 0;
            while (reader.Read())
            {
                GiaHangGhe = decimal.Parse(reader["Gia"].ToString());
            }
            reader.Close();

            //Tao hoa don
            PHoaDon hoaDon = new PHoaDon();
            hoaDon.NgayDat = DateTime.Now;
            hoaDon.TinhTongTien((float)thongTinChuyenBay.GiaBay, (float)GiaHangGhe);

            TempData["ThongTinChuyenBay"] = thongTinChuyenBay;
            TempData["DanhSachHanhKhach"] = hanhKhach;
            TempData["MaHG"] = maHG;
            TempData["HoaDon"] = hoaDon;
            ViewBag.SLKhach = hanhKhach.Count();

            return View(hoaDon);
        }
        public ActionResult LichSuDatVe()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            string userName = Session["UserName"].ToString();
            List<LichSuDatVe> lichSuDatVes = new List<LichSuDatVe>();

            string sqlQuery = @"
                SELECT pd.MaPhieuDat, pd.NgayDat, cb.MaChuyenBay, cb.NgayGioDi, 
                       mb.TenMayBay, sbDi.TenSanBay AS SanBayDi, sbDen.TenSanBay AS SanBayDen
                FROM PhieuDat pd
                JOIN Ve v ON pd.MaPhieuDat = v.MaPhieuDat
                JOIN ChuyenBay cb ON v.MaChuyenBay = cb.MaChuyenBay
                JOIN MayBay mb ON cb.MaMayBay = mb.MaMayBay
                JOIN SanBay sbDi ON cb.MaSanBayDi = sbDi.MaSanBay
                JOIN SanBay sbDen ON cb.MaSanBayDen = sbDen.MaSanBay
                JOIN KhachHang kh ON pd.MaKhachHang = kh.MaKhachHang
                JOIN TaiKhoan tk ON kh.MaTaiKhoan = tk.MaTaiKhoan
                WHERE tk.TenTaiKhoan = @TenTaiKhoan";

            DBConnect db = new DBConnect();
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@TenTaiKhoan", userName)
            };

            using (SqlDataReader reader = db.ThucThiReader(sqlQuery, sqlParameters))
            {
                while (reader.Read())
                {
                    LichSuDatVe lichSu = new LichSuDatVe
                    {
                        MaPhieuDat = Convert.ToInt32(reader["MaPhieuDat"]),
                        NgayDat = Convert.ToDateTime(reader["NgayDat"]),
                        MaChuyenBay = reader["MaChuyenBay"].ToString(),
                        NgayGioDi = Convert.ToDateTime(reader["NgayGioDi"]),
                        TenMayBay = reader["TenMayBay"].ToString(),
                        SanBayDi = reader["SanBayDi"].ToString(),
                        SanBayDen = reader["SanBayDen"].ToString()
                    };
                    lichSuDatVes.Add(lichSu);
                }
            }
            return View("LichSuDatVe",lichSuDatVes);
        }


    }
}
