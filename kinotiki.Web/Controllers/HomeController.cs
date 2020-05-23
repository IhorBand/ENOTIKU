using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinotiki.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(int pageSize = 9, int pageNum = 0)
        {
            ViewBag.SizeOfPosts = pageSize;
            ViewBag.CurPageNum = pageNum;
            return View();
        }
    }
}