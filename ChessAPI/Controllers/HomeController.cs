using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessAPI.Controllers
{
    public class HomeController : Controller
    {
        private AuthHelper authHelper;
        public HomeController(AuthHelper authHelper)
        {
            this.authHelper = authHelper;
        }
        public ActionResult Index()
        {
            authHelper.SetSessionCookie(Request, Response, "a");
            return View();
        }
    }
}
