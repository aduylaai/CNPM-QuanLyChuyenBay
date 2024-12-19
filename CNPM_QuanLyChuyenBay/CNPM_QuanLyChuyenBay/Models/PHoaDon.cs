using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class PHoaDon
    {
        public int MaPhieuDat { get; set; }
        public float TongTien { get; set; }
        public DateTime NgayDat { get; set; }

        public void TinhTongTien(float GiaBay, float GiaHangGhe, int SoLuongKhach)
        {
            TongTien = 0;
            TongTien += (GiaBay + GiaHangGhe) * SoLuongKhach;
        }

    }
}