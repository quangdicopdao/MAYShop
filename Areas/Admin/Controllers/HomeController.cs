using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAYShop.Models;

namespace MAYShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        MAYshopDataContext db = new MAYshopDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        //product
       
        public ActionResult NavPartial()
        {
            return PartialView();
        }
    }
}