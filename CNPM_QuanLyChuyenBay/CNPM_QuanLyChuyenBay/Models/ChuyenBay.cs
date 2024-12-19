using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class ChuyenBay
    {
        public int MaChuyenBay { get; set; }
        public int MaHangHangKhong { get; set; }
        public int MaTrangThaiChuyenBay { get; set; }
        public int MaLoTrinh { get; set; }
        public int MaMayBay { get; set; }
        public decimal GiaBay { get; set; }
        public int? SLGhePhoThong { get; set; }
        public int? SLGheThuongGia { get; set; }
        public DateTime? NgayGioDi { get; set; }
        public DateTime? NgayGioDen { get; set; }

    }
}