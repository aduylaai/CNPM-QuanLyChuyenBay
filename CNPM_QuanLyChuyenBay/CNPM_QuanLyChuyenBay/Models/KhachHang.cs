using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class KhachHang
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public int MaTaiKhoan { get; set; }
    }
}