using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class LichSuDatVe
    {
        public int MaPhieuDat { get; set; }
        public DateTime NgayDat { get; set; }
        public string MaChuyenBay { get; set; }
        public DateTime NgayGioDi { get; set; }
        public string TenMayBay { get; set; }
        public string SanBayDi { get; set; }
        public string SanBayDen { get; set; }

        public string MaBooking { get; set; }
    }

}