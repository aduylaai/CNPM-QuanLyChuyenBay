using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class HanhKhach
    {
        public int MaHanhKhach { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public string QuocTich { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string CCCD_Passport { get; set; }
        public int MaKhachHang { get; set; }
    }
}