using CNPM_QuanLyChuyenBay.Helpers;
using CNPM_QuanLyChuyenBay.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CNPM_QuanLyChuyenBay.Controllers
{
    public class ChuyenBayController : Controller
    {
        DBConnect dBConn = new DBConnect(@".", "CNPM_QuanLyBanVeMayBay");
        // GET: ChuyenBay
        public ActionResult Index()
        {
            List<KetQuaTimKiem> dsChuyenBay = new List<KetQuaTimKiem>();

            // Mở kết nối với cơ sở dữ liệu
            dBConn.openConnect();

            try
            {
                // Tạo câu truy vấn để lấy tất cả chuyến bay
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
                SanBay sb_den ON sb_den.MaSanBay = lt.MaSB_Den";

                // Thực thi câu truy vấn và đọc kết quả
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

                        dsChuyenBay.Add(item);
                    }
                }

                return View(dsChuyenBay); // Trả về view với danh sách chuyến bay
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw;
            }
            finally
            {
                dBConn.closeConnect(); // Đảm bảo đóng kết nối
            }
        }


      
        //        public ActionResult Details(int id)
        //{
        //    ChuyenBay cb = null;
        //    try
        //    {
        //        SqlDataReader reader = dbConn.ThucThiReader($"SELECT * FROM ChuyenBay WHERE MaChuyenBay = {id}");
        //        if (reader.Read())
        //        {
        //            cb = new ChuyenBay
        //            {
        //                MaChuyenBay = int.Parse(reader["MaChuyenBay"].ToString()),
        //                MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString()),
        //                MaTrangThaiChuyenBay = int.Parse(reader["MaTrangThaiChuyenBay"].ToString()),
        //                MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString()),
        //                MaMayBay = int.Parse(reader["MaMayBay"].ToString()),
        //                GiaBay = decimal.Parse(reader["GiaBay"].ToString()),
        //                SLGhePhoThong = int.Parse(reader["SLGhePhoThong"].ToString()),
        //                SLGheThuongGia = int.Parse(reader["SLGheThuongGia"].ToString()),
        //                NgayGioDi = DateTime.Parse(reader["NgayGioDi"].ToString()),
        //                NgayGioDen = DateTime.Parse(reader["NgayGioDen"].ToString())
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        dbConn.conn.Close();
        //    }

        //    return View(cb);
        //}

        // GET: ChuyenBay/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: ChuyenBay/Create
        [HttpPost]
        public ActionResult Create(ChuyenBay cb)
        {
            try
            {
                dBConn.openConnect();
                // Chèn chuyến bay mới vào cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("INSERT INTO ChuyenBay (MaHangHangKhong, MaTrangThaiChuyenBay, MaLoTrinh, MaMayBay, GiaBay, SLGhePhoThong, SLGheThuongGia, NgayGioDi, NgayGioDen) " +"VALUES (@MaHangHangKhong, @MaTrangThaiChuyenBay, @MaLoTrinh, @MaMayBay, @GiaBay, @SLGhePhoThong, @SLGheThuongGia, @NgayGioDi, @NgayGioDen)", dBConn.conn);

                cmd.Parameters.AddWithValue("@MaHangHangKhong", cb.MaHangHangKhong);
                cmd.Parameters.AddWithValue("@MaTrangThaiChuyenBay", cb.MaTrangThaiChuyenBay);
                cmd.Parameters.AddWithValue("@MaLoTrinh", cb.MaLoTrinh);
                cmd.Parameters.AddWithValue("@MaMayBay", cb.MaMayBay);
                cmd.Parameters.AddWithValue("@GiaBay", cb.GiaBay);
                cmd.Parameters.AddWithValue("@SLGhePhoThong", cb.SLGhePhoThong ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SLGheThuongGia", cb.SLGheThuongGia ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayGioDi", cb.NgayGioDi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayGioDen", cb.NgayGioDen ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();

                // Sau khi thêm, chuyển hướng về trang Index
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // Nếu có lỗi, trả lại trang Create
                return View(cb);
            }
        }


        // GET: ChuyenBay/Edit/5
        // GET: ChuyenBay/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    // Tìm chuyến bay theo id
        //    SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM ChuyenBay WHERE MaChuyenBay = " + id);
        //    if (reader.Read())
        //    {
        //        ChuyenBay cb = new ChuyenBay
        //        {
        //            MaChuyenBay = int.Parse(reader["MaChuyenBay"].ToString()),
        //            MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString()),
        //            MaTrangThaiChuyenBay = int.Parse(reader["MaTrangThaiChuyenBay"].ToString()),
        //            MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString()),
        //            MaMayBay = int.Parse(reader["MaMayBay"].ToString()),
        //            GiaBay = decimal.Parse(reader["GiaBay"].ToString()),
        //            SLGhePhoThong = reader["SLGhePhoThong"] != DBNull.Value ? (int?)int.Parse(reader["SLGhePhoThong"].ToString()) : null,
        //            SLGheThuongGia = reader["SLGheThuongGia"] != DBNull.Value ? (int?)int.Parse(reader["SLGheThuongGia"].ToString()) : null,
        //            NgayGioDi = reader["NgayGioDi"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["NgayGioDi"].ToString()) : null,
        //            NgayGioDen = reader["NgayGioDen"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["NgayGioDen"].ToString()) : null
        //        };
        //        return View(cb);
        //    }
        //    else
        //    {
        //        // Nếu không tìm thấy chuyến bay, chuyển hướng về danh sách
        //        return RedirectToAction("Index");
        //    }
        //}


        //// POST: ChuyenBay/Edit/5
        //// POST: ChuyenBay/Edit/5
        //[HttpPost]
        //public ActionResult Edit(ChuyenBay cb)
        //{
        //    try
        //    {
        //        // Cập nhật thông tin chuyến bay
        //        SqlCommand cmd = new SqlCommand("UPDATE ChuyenBay SET MaHangHangKhong = @MaHangHangKhong, MaTrangThaiChuyenBay = @MaTrangThaiChuyenBay, MaLoTrinh = @MaLoTrinh, MaMayBay = @MaMayBay, GiaBay = @GiaBay, SLGhePhoThong = @SLGhePhoThong, SLGheThuongGia = @SLGheThuongGia, NgayGioDi = @NgayGioDi, NgayGioDen = @NgayGioDen WHERE MaChuyenBay = @MaChuyenBay", dbConn.conn);
        //        cmd.Parameters.AddWithValue("@MaChuyenBay", cb.MaChuyenBay);
        //        cmd.Parameters.AddWithValue("@MaHangHangKhong", cb.MaHangHangKhong);
        //        cmd.Parameters.AddWithValue("@MaTrangThaiChuyenBay", cb.MaTrangThaiChuyenBay);
        //        cmd.Parameters.AddWithValue("@MaLoTrinh", cb.MaLoTrinh);
        //        cmd.Parameters.AddWithValue("@MaMayBay", cb.MaMayBay);
        //        cmd.Parameters.AddWithValue("@GiaBay", cb.GiaBay);
        //        cmd.Parameters.AddWithValue("@SLGhePhoThong", cb.SLGhePhoThong ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@SLGheThuongGia", cb.SLGheThuongGia ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@NgayGioDi", cb.NgayGioDi ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@NgayGioDen", cb.NgayGioDen ?? (object)DBNull.Value);
        //        cmd.ExecuteNonQuery();

        //        // Sau khi cập nhật, chuyển hướng về trang Index
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        // Nếu có lỗi, trả lại trang Edit
        //        return View(cb);
        //    }
        //}


        //// GET: ChuyenBay/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    // Tìm chuyến bay theo id
        //    SqlDataReader reader = dbConn.ThucThiReader("SELECT * FROM ChuyenBay WHERE MaChuyenBay = " + id);
        //    if (reader.Read())
        //    {
        //        ChuyenBay cb = new ChuyenBay
        //        {
        //            MaChuyenBay = int.Parse(reader["MaChuyenBay"].ToString()),
        //            MaHangHangKhong = int.Parse(reader["MaHangHangKhong"].ToString()),
        //            MaTrangThaiChuyenBay = int.Parse(reader["MaTrangThaiChuyenBay"].ToString()),
        //            MaLoTrinh = int.Parse(reader["MaLoTrinh"].ToString()),
        //            MaMayBay = int.Parse(reader["MaMayBay"].ToString()),
        //            GiaBay = decimal.Parse(reader["GiaBay"].ToString()),
        //            SLGhePhoThong = reader["SLGhePhoThong"] != DBNull.Value ? (int?)int.Parse(reader["SLGhePhoThong"].ToString()) : null,
        //            SLGheThuongGia = reader["SLGheThuongGia"] != DBNull.Value ? (int?)int.Parse(reader["SLGheThuongGia"].ToString()) : null,
        //            NgayGioDi = reader["NgayGioDi"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["NgayGioDi"].ToString()) : null,
        //            NgayGioDen = reader["NgayGioDen"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["NgayGioDen"].ToString()) : null
        //        };
        //        return View(cb);
        //    }
        //    else
        //    {
        //        // Nếu không tìm thấy chuyến bay, chuyển hướng về danh sách
        //        return RedirectToAction("Index");
        //    }
        //}

        //// POST: ChuyenBay/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // Xóa chuyến bay
        //        SqlCommand cmd = new SqlCommand("DELETE FROM ChuyenBay WHERE MaChuyenBay = @MaChuyenBay", dbConn.conn);
        //        cmd.Parameters.AddWithValue("@MaChuyenBay", id);
        //        cmd.ExecuteNonQuery();

        //        // Sau khi xóa, chuyển hướng về trang Index
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        // Nếu có lỗi, chuyển về trang Delete
        //        return View();
        //    }
        //}

    }
}
