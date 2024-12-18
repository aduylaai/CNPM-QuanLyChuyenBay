using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class PhieuDat
    {
        public int MaPhieuDat { get; set; }
        public int MaKhachHang { get; set; }
        public DateTime NgayDat { get; set; }
        public int MaBooking { get; set; }
    }
}