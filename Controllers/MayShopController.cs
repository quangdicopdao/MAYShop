using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;
using PagedList;
using PagedList.Mvc;
namespace MAYShop.Controllers
{
    public class MayShopController : Controller
    {
        MAYshopDataContext db = new MAYshopDataContext();
        // GET: MayShop
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection f)
        {
            Contact c = new Contact();
            c.Name = f["txtName"];
            c.Email = f["txtEmail"];
            c.Body = f["txtMessage"];
            db.Contacts.InsertOnSubmit(c);
            db.SubmitChanges();
            return View();
        }
        public ActionResult BrandPartial()
        {
            var listBrand = from b in db.Brands select b;
            return PartialView(listBrand);
        }
        private List<Product> GetNewProduct(int count)
        {
            return db.Products.OrderByDescending(s => s.CreateDate).Take(count).ToList();
        }
       public ActionResult Shop(int ? page, string str)
        {
            int iSize = 20;
            var iPageNum = (page ?? 1);
            var list = GetNewProduct(20);
            switch (str)
            {
                case "Tang":
                    {
                        list = list.OrderBy(s => s.Price).ToList();
                        break;
                    }
                case "Giam":
                    {
                        list = list.OrderByDescending(s => s.Price).ToList();
                        break;
                    }

            }
            return View(list.ToPagedList(iPageNum,iSize));
           
        }
        public ActionResult HeaderPartial()
        {
            return PartialView();
        }
        public ActionResult ChiTietSanPham(int id)
        {
            return View(db.Products.Where(sp => sp.ProductID == id).SingleOrDefault());
        }
        public ActionResult Search(string str,int ? page)
        {
            int iSize = 20;
            var iPageNum = (page ?? 1);
            if (!string.IsNullOrEmpty(str))
            {
                var kq = from sp in db.Products where sp.Title.Contains(str) select sp;
                ViewBag.Search = str;
                
                return View(kq.ToPagedList(iPageNum,iSize));
            }
            return View();
        }
        public ActionResult SearchPartial()
        {
            return PartialView();
        }
        public ActionResult TagPartial()
        {
            var listBrand = from b in db.Categories select b;
            return PartialView(listBrand);
        }
        public ActionResult SanPhamTheoTag(int id, int? page)
        {
            ViewBag.TagID = id;
            int iSize = 3;
            var iPageNum = (page ?? 1);
            var kq = from sp in db.Products where sp.CatogeryID == id select sp;
            return View(kq.ToPagedList(iPageNum, iSize));
        }
        public ActionResult SanPhamTheoBrand(int id,int ? page)
        {
            ViewBag.BrandID = id;
            int iSize = 3;
            var iPageNum = (page ?? 1);
            var kq = from sp in db.Products where sp.BrandID == id select sp;
            return View(kq.ToPagedList(iPageNum,iSize));
        }
        public ActionResult SizePartial()
        {
            var list = from s in db.Sizes select s;
            return PartialView(list);
        }
        // gop parrtial
        public ActionResult SanPhamTheoSize(int id,int ? page)
        {
            ViewBag.SizeID = id;
            int iSize = 3;
            var iPageNum = (page ?? 1);
            var kq = from s in db.Products where s.SizeID == id select s;
            return View(kq.ToPagedList(iPageNum, iSize));
        }

    }
}