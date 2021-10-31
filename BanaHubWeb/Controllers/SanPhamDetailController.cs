using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;

namespace BanaHubWeb.Controllers
{
    public class SanPhamDetailController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: SanPhamDetail
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult XuLyPartial(int id, int? page)
        {
            int iPageSize = 16;
            int iPageNum = (page ?? 1);
            ViewBag.MaLoai = id;
            var list = from l in data.SANPHAMs where l.MaLoai == id select l;

            return View(list.ToPagedList(iPageNum, iPageSize));
        }

        public ActionResult SanPhamLinkPartial()
        {
            var ProductType = from sp in data.SANPHAMs
                              group new { sp.LOAI, sp } by new
                              {
                                  sp.LOAI.MaLoai,
                                  sp.LOAI.TenLoai
                              } into g
                              select new ProductLink
                              {
                                  ID = (int)g.Key.MaLoai,
                                  ProductName = g.Key.TenLoai,
                                  Quantity = (int)g.Count(p => p.sp.MaLoai != null)
                              };

            return View(ProductType);
        }


        public ActionResult ChiTietSanPham(int id, int maloai)
        {
            /*var list = from l in data.SANPHAMs where l.MaSP == id select l;*/
            int average;
            var checkNull = (from t in data.COMMENTs where t.ProductID == id select t).Count();
            if(checkNull == 0)
            {
                average = 0;
            }
            else
            {
                average = (int)(from s in data.COMMENTs where s.ProductID == id select s.AuthorRating).Average();
            }
            
            ViewBag.MaLoai = maloai;

            var list = from sp in data.SANPHAMs
                       where sp.MaSP == id
                       select new ProductDetail
                       {
                           MaSP = sp.MaSP,
                           TenSP = sp.TenSP,
                           MoTa = sp.MoTa,
                           SoLuongBan = (int)sp.SoLuongBan,
                           GiaBan = (long)sp.GiaBan,  
                           Average = average
                       };

            return View(list);
        }

        public ActionResult MoreImage(int id)
        {
            var list = from l in data.MOREIMAGEs where l.MaSP == id select l;
            return View(list);
        }

        public ActionResult Comment(int id, int maloai)
        {
            var comment = from cm in data.COMMENTs where cm.ProductID == id orderby cm.CommentTime descending select cm;
            ViewBag.ProductID = id;
            ViewBag.MaLoai = maloai;

            return View(comment);
        }

        [HttpPost]
        public ActionResult AddComment(int authorRating, string authorComment, string authorName, string authorEmail, int productID, int typeProID)
        {
            COMMENT comment = new COMMENT();
            comment.AuthorName = authorName;
            comment.AuthorEmail = authorEmail;
            comment.AuthorComment = authorComment;
            comment.AuthorRating = authorRating;
            comment.CommentTime = DateTime.Now;
            comment.ProductID = productID;

            data.COMMENTs.InsertOnSubmit(comment);
            data.SubmitChanges();
            
            return Redirect("/SanPhamDetail/ChiTietSanPham?id=" + productID + "&maloai=" + typeProID);
        }    
    }
}