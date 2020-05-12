using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kinotiki.Domain.Entity;
using kinotiki.BLL.Abstract;
using System.Web.Security;

namespace kinotiki.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated == true)
                return RedirectToAction("Logout", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.Account.RegisterUser model, HttpPostedFileBase image)
        {
            if (User.Identity.IsAuthenticated == true)
                return RedirectToAction("Logout","Account");
            if (ModelState.IsValid
                && userService.Find(model.login) == null
                && userService.FindByMail(model.email) == null)
            {
                if(image != null)
                {
                    model.imageMimeType = image.ContentType;
                    model.imageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.imageData, 0, image.ContentLength);
                }

                model.password = Helpers.AuthHelper.EncodePassword(model.password);

                userService.Create(new BLL.Entity.User
                {
                    login = model.login,
                    password = model.password,
                    email = model.email,
                    age = model.age,
                    sex = (BLL.Entity.Enums.GenderType)model.sex,
                    role = BLL.Entity.Enums.RoleType.User,
                    imageMimeType = model.imageMimeType,
                    imageData = model.imageData
                });

                if (userService.Find(model.login) != null)
                {
                    FormsAuthentication.SetAuthCookie(model.login, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login","Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Account.LoginUser model)
        {
            if(ModelState.IsValid)
            {
                model.password = Helpers.AuthHelper.EncodePassword(model.password);
                if (userService.Find(model.login, model.password) != null)
                {
                    FormsAuthentication.SetAuthCookie(model.login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("WrongLogin", "Wrong Login or Password");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public ActionResult EditAccount()
        {
            var user = userService.Find(User.Identity.Name);
            user.password = Helpers.AuthHelper.DecodePassword(user.password);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditAccount(BLL.Entity.User user, HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    user.imageMimeType = image.ContentType;
                    user.imageData = new byte[image.ContentLength];
                    image.InputStream.Read(user.imageData, 0, image.ContentLength);
                }
                else
                {
                    var curUser = userService.Find(User.Identity.Name);
                    if (curUser.imageData != null)
                    {
                        user.imageMimeType = curUser.imageMimeType;
                        user.imageData = curUser.imageData;
                    }
                }

                user.password = Helpers.AuthHelper.EncodePassword(user.password);

                if (userService.Update(user))
                {
                    FormsAuthentication.SetAuthCookie(user.login, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        public PartialViewResult LoginPartial()
        {
            return PartialView("LoginPartial", new Models.Account.LoginUser());
        }

        public PartialViewResult GetInformationSideMenuBox()
        {
            return PartialView("GetInformationSideMenuBox", userService.Find(User.Identity.Name));
        }

        public FileContentResult GetImage(string login)
        {
            BLL.Entity.User user = userService.Find(login);
            if (user != null && user.imageData != null && user.imageData.Length > 0 && !string.IsNullOrEmpty(user.imageMimeType))
                return File(user.imageData, user.imageMimeType);
            return null;
        }
    }
}