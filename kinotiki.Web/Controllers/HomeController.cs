using AutoMapper;
using kinotiki.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinotiki.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService;
        private IMapper mapper;

        public HomeController(IUserService userService,
            IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        public ActionResult Index(int pageSize = 9, int pageNum = 0)
        {
            ViewBag.SizeOfPosts = pageSize;
            ViewBag.CurPageNum = pageNum;
            return View();
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        public ActionResult GetTheme()
        {
            bool isDarkTheme = true;
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = userService.Find(User.Identity.Name);
                if(user != null && user.id > 0)
                {
                    isDarkTheme = user.isDarkTheme;
                }
            }
            return PartialView("GetTheme", isDarkTheme);
        }
    }
}