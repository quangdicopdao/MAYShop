using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;

namespace MAYShop.Controllers
{
    public class LoveProductController : Controller
    {
        MAYshopDataContext db = new MAYshopDataContext();
        // GET: LoveProduct
        public List<LoveItem> ListLove()
        {
            List<LoveItem> lst = Session["item"] as List<LoveItem>;
            if (lst == null)
            {
                lst = new List<LoveItem>();
                Session["item"] = lst;
            }
            return lst;
        }
        public ActionResult AddLoveProduct(int id, string url)
        {
            LoveProduct l = (LoveProduct)Session["LoveID"];
            List<LoveItem> lst = ListLove();
            LoveItem sp = lst.Find(s => s.ProductID == id);
            if (sp == null)
            {
                sp = new LoveItem(id,l.LoveID);
                lst.Add(sp);
                
            }
            LoveItem lv = new LoveItem();
            foreach (var item in lst)
            {
                lv.LoveID = l.LoveID;
                lv.ProductID = item.ProductID;
                lv.Picture = item.Picture;
                lv.Title = item.Title;
                lv.Price = item.Price;
            }
            db.LoveItems.InsertOnSubmit(lv);
            db.SubmitChanges();
            return Redirect(url);
        }
        public ActionResult LoveItemPage()
        {
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap","User");
            }
            LoveProduct lst = (LoveProduct)Session["LoveID"];
            return View(db.LoveItems.Where(n => n.LoveID == lst.LoveID).GroupBy(m=>m.ProductID).Select(a=>a.First()).ToList());
        }
        public ActionResult DeleteLoveItem()
        {
            List<LoveItem> lst = ListLove();
            lst.Clear();
            LoveProduct l = (LoveProduct)Session["LoveID"];
            var kq = from s in db.LoveItems where s.LoveID == l.LoveID select s;
            foreach(var item in kq)
            {
                db.LoveItems.DeleteOnSubmit(item);
            }
            try
            {
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Shop", "MayShop");
        }
        public ActionResult DeleteItem(int id)
        {
            List<LoveItem> lst = ListLove();
            LoveItem sp = lst.SingleOrDefault(n => n.ProductID == id);
            var kq = from s in db.LoveItems where s.ProductID == id select s;
            foreach(var item in kq)
            {
                db.LoveItems.DeleteOnSubmit(item);
            }
            try
            {
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (sp != null)
            {
                lst.RemoveAll(n => n.ProductID == id);
                if (lst.Count == 0)
                {
                    return RedirectToAction("Shop", "MayShop");
                }
            }
            return RedirectToAction("LoveItemPage");
        }
    }
}