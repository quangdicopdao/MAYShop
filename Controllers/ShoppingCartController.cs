using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;

namespace MAYShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        MAYshopDataContext db = new MAYshopDataContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
           if(Session["TaiKhoan"]== null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            else
            {
                Cart c = (Cart)Session["CartID"];
                List<CartItem> lst = ListCart();
                if (lst.Count() == 0)
                {
                    return RedirectToAction("Shop", "MayShop");
                }
                ViewBag.TongSoLuong = TotalQuantity();
                ViewBag.TongTien = TotalMoney();
                return View(lst);
            }
                
            
        }
        
        // GET: LoveProduct
        public List<CartItem> ListCart()
        {
            Cart c = (Cart)Session["CartID"];
            return db.CartItems.Where(n => n.CartID == c.CartID).GroupBy(m => m.ProductID).Select(a => a.First()).ToList();
        }
        public ActionResult AddCart(int id,string url,FormCollection f)
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            else
            {
                Cart c = (Cart)Session["CartID"];
                List<CartItem> lst = ListCart();
                CartItem sp = lst.Find(n => n.ProductID == id);
                CartItem ci = new CartItem();
                if (sp == null)
                {
                    sp = new CartItem(id, c.CartID);
                    lst.Add(sp);

                    foreach (var item in lst)
                    {
                        ci.CartID = c.CartID;
                        ci.ProductID = item.ProductID;
                        ci.Title = item.Title;
                        ci.Price = item.Price;
                        ci.Picture = item.Picture;
                        ci.Quantity = item.Quantity;
                        ci.TotalMoney = item.Price * item.Quantity;
                    }
                    db.CartItems.InsertOnSubmit(ci);
                }
                else
                {
                    var p = db.CartItems.SingleOrDefault(n => n.ProductID == id);
                    p.Quantity = p.Quantity + 1;
                    p.TotalMoney = p.Quantity * p.Price;
                    db.SubmitChanges();
                }
                db.SubmitChanges();
                return Redirect(url);
            }
        }
        private int TotalQuantity()
        {
            int count = 0; 
            Cart c = (Cart)Session["CartID"];
            var kq = (from s in db.CartItems where s.CartID == c.CartID select s).ToList().Sum(n=>n.Quantity);
            count = Convert.ToInt32(kq);
            return count;
        }
        private double TotalMoney()
        {
            double dTongTien = 0;
            Cart c = (Cart)Session["CartID"];
            var kq = (from s in db.CartItems where s.CartID == c.CartID select s).ToList().Sum(n => n.Quantity* n.Price);
            dTongTien = Convert.ToDouble(kq);
            return dTongTien;
        }

        public ActionResult DeleteItem(int id)
        {
            List<CartItem> lst = ListCart();
            CartItem c = lst.SingleOrDefault(n => n.ProductID == id);
            var kq = from s in db.CartItems where s.ProductID == id select s;
            foreach(var i in kq)
            {
                db.CartItems.DeleteOnSubmit(i);
            }
            try
            {
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            if (c != null)
            {
                lst.RemoveAll(n => n.ProductID == id);
                if(lst.Count == 0)
                {
                    return RedirectToAction("Shop", "MayShop");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart(int id , FormCollection f)
        {
            List<CartItem> lst = ListCart();
            CartItem c = lst.SingleOrDefault(n => n.ProductID == id);
            c.Quantity = Convert.ToInt32(f["txtSoLuong"].ToString());
            c.TotalMoney = c.Quantity * c.Price;
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCart()
        {
            List<CartItem> lst = ListCart();
            Cart c = (Cart)Session["CartID"];
            var kq = from s in db.CartItems where s.CartID == c.CartID select s;
            foreach (var item in kq)
            {
                db.CartItems.DeleteOnSubmit(item);
            }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            lst.Clear();
            return RedirectToAction("Shop", "MayShop");
        }
        public ActionResult ShoppingCartPartial()
        {
            ViewBag.TongSoLuong = TotalQuantity();
            ViewBag.TongTien = TotalMoney();
            return PartialView();
        }
        [HttpGet]
        public ActionResult CheckOut()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }

            // kiểm tra có hàng trong giỏ hàng ch
           
            List<CartItem> lst = ListCart();
            ViewBag.TongSoLuong = TotalQuantity();
            ViewBag.TongTien = TotalMoney();
            return View(lst);
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection f)
        {
            Order o = new Order();
            Customer c = (Customer)Session["TaiKhoan"];
            List<CartItem> lst = ListCart();
            o.CustomerID = c.CustomerID;
            o.DayAdd = DateTime.Now;
            var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            o.DayGo = DateTime.Parse(NgayGiao);
            o.StatusTrans = 1;
            o.StatusPay = false;
            db.Orders.InsertOnSubmit(o);
            db.SubmitChanges();
            foreach(var item in lst)
            {
                OrderDetail od = new OrderDetail();
                od.OrderID = o.OrderID;
                od.ProductID = item.ProductID;
                od.Quantity = item.Quantity;
                od.Price = (decimal)item.Price;
                db.OrderDetails.InsertOnSubmit(od);
            }
            db.SubmitChanges();
            return RedirectToAction("ConfirmOrder", "ShoppingCart");
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
    }
}