using kinotiki.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinotiki.Web.Controllers
{
    public class PostController : Controller
    {
        public PostController()
        {
        }

        public ActionResult GetAllHeaderPosts(int pageSize = 9, int pageNum = 0)
        {
            List<string> headerPosts = new List<string>();

            return View(headerPosts);
        }
    }
}