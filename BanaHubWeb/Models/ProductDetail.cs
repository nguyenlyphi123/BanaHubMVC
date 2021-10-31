using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class ProductDetail
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public long GiaBan { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongBan { get; set; }
        public string HinhChinh { get; set; }
        public string Hinh2 { get; set; }
        public string Hinh3 { get; set; }
        public string Hinh4 { get; set; }
        public int CommentID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorComment { get; set; }
        public string AuthorEmail { get; set; }
        public int AuthorRating { get; set; }
        public DateTime CommentTime { get; set; }
        public int Average { get; set; }

    }
}