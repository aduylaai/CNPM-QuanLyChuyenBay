using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Models
{
    public class LoTrinh
    {
        public int MaLoTrinh { get; set; }
        public int MaSB_Di { get; set; }
        public int MaSB_Den { get; set; }

        public string TenSanBayDi { get; set; }
        public string TenSanBayDen { get; set; }


    }
}