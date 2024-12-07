using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CNPM_QuanLyChuyenBay.Helpers
{
    public class CacHamKiemTra
    {

        //Kiểm tra chuỗi sau khi xoá hết khoảng trắng có rỗng
        public static string KiemTraChuoiRong(string str)
        {
            if (str.Trim().Length == 0) throw new Exception("Hãy nhập đủ thông tin!");
            return str;
        }
        //
        public static string KiemTraChuoi(string input)
        {
            string output = KiemTraChuoiRong(input);

            string pattern = @"[;\-'%_()]+";

            if (Regex.IsMatch(input, pattern)) throw new Exception("Chuỗi không được chứa các ký tự \"" + pattern + "\"");
            return output;
        }

    }
}