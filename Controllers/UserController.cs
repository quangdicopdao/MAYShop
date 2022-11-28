using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;
using System.Threading.Tasks;
namespace MAYShop.Controllers
{
    public class UserController : Controller
    {
        MAYshopDataContext db = new MAYshopDataContext();
        // GET: User
        
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {

                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Customer kh = db.Customers.SingleOrDefault(n => n.UserName == sTenDN && n.Password == sMatKhau);
                LoveProduct l = db.LoveProducts.SingleOrDefault(n=>n.CustomerID == kh.CustomerID);
                Cart c = db.Carts.SingleOrDefault(n => n.CustomerID == kh.CustomerID);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    Session["LoveID"] = l;
                    Session["CartID"] = c;
                    return RedirectToAction("Index", "MayShop");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";

                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "MayShop");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, Customer kh)
        {
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var sNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (db.Customers.SingleOrDefault(n => n.UserName == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.Customers.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã sử dụng";
            }
            else if (ModelState.IsValid)
            {
                kh.FullName = sHoTen;
                kh.UserName = sTenDN;
                kh.Password = sMatKhau;
                kh.Email = sEmail;
                kh.Address = sDiaChi;
                kh.PhoneNumber = sDienThoai;
                kh.DateOfBirth = DateTime.Parse(sNgaySinh);
                db.Customers.InsertOnSubmit(kh);
                db.SubmitChanges();
                LoveProduct l = new LoveProduct();
                Cart c = new Cart();
                l.CustomerID = kh.CustomerID;
                db.LoveProducts.InsertOnSubmit(l);
                db.SubmitChanges();
                c.CustomerID = kh.CustomerID;
                db.Carts.InsertOnSubmit(c);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ResetPassword(FormCollection f)
        {
            Customer c = (Customer)Session["TaiKhoan"];
            var user = db.Customers.SingleOrDefault(n => n.CustomerID==c.CustomerID);
                user.Password = f["MatKhauMoi"];
                db.SubmitChanges();
                return RedirectToAction("DangNhap", "User");
        }
    }
}