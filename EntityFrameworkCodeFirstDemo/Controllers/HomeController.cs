using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkCodeFirstDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult First()
        {
            // C:\Users\Anamika\AppData\Local\Google\Chrome\User Data\Default
            HttpCookie cookie = new HttpCookie("TestCookie");
            cookie.Value = "This is test cookie";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            ViewBag.cookie = cookie;
            return RedirectToAction("Index","Users");
        }
        public ActionResult Index()
        {
            //if (!HttpContext.Request.Cookies.ContainsKey("first_request"))
            //{
            //    HttpContext.Response.Cookies.Append("first_request", DateTime.Now.ToString());
            //    return Content("Welcome, new visitor!");
            //}
            //else
            //{
            //    DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["first_request"]);
            //    return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString());
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}