using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class KetQuaTimKiem
    {
        public int MaChuyenBay { get; set; }
        public string TenHangHangKhong { get; set; }
        public string TenSB_Di { get; set; }
        public string TenSB_Den { get; set; }
        public int SLGhePhoThong { get; set; }
        public int SLGheThuongGia { get; set; }
        public DateTime NgayGioDi { get; set; }
        public DateTime NgayGioDen { get; set; }
        public decimal GiaBay { get; set; }
    }
}