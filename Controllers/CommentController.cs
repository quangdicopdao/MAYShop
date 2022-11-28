 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;
namespace MAYShop.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        MAYshopDataContext db = new MAYshopDataContext();
        
        [HttpGet]
        [ChildActionOnly]
        public ActionResult BinhLuan(int id)
        {
            var binhluan = from bl in db.FeedBacks where bl.ProductID == id select bl;
            return PartialView(binhluan);
        }
        [HttpPost]
        public ActionResult BinhLuan(int id, FormCollection f, FeedBack bl)
        {
            var sHoTen = f["HoTen"];
            var sEmail = f["Email"];
            var sNoiDung = f["NoiDung"];
            var iDanhGia = f["DanhGia"];
            if (ModelState.IsValid)
            {
                bl.FullName = sHoTen;
                bl.Email = sEmail;
                bl.Note = sNoiDung;
                bl.Rating = int.Parse(iDanhGia);
                bl.DateRate = DateTime.Now;
                bl.ProductID = id;
                db.FeedBacks.InsertOnSubmit(bl);
                db.SubmitChanges();
                return Redirect("~/MayShop/ChiTietSanPham/" + id);
            }
            return PartialView();
        }
    }
}