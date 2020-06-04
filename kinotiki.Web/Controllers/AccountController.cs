using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kinotiki.Domain.Entity;
using kinotiki.BLL.Abstract;
using System.Web.Security;
using AutoMapper;
using kinotiki.BLL.EmailHelper;

namespace kinotiki.Web.Controllers
{
    public class AccountController : Controller
    {
        private IGlobalSettingsService globalSettingsService;
        private IUserService userService;
        private IMapper mapper;

        public AccountController(IGlobalSettingsService globalSettingsService,
            IUserService userService, 
            IMapper mapper)
        {
            this.globalSettingsService = globalSettingsService;
            this.userService = userService;
            this.mapper = mapper;
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
            if (ModelState.IsValid)
            {
                if (userService.Find(model.login) != null)
                {
                    ModelState.AddModelError("UserLoginAlreadyExist", "User With This Login Already Exists.");
                    return View(model);
                }
                if (userService.FindByMail(model.email) != null)
                {
                    ModelState.AddModelError("UserEmailAlreadyExist", "User With This Email Already Exists.");
                    return View(model);
                }

                if (image != null)
                {
                    model.imageMimeType = image.ContentType;
                    model.imageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.imageData, 0, image.ContentLength);
                }

                model.password = Helpers.AuthHelper.EncodePassword(model.password);

                //need to check AutoMapper
                //TODO: Something went wrong with Automapper after last commits
                //var userModel = mapper.Map<BLL.Entity.User>(model);
                var userModel = new BLL.Entity.User()
                {
                    login = model.login,
                    email = model.email,
                    password = model.password,
                    sex = (BLL.Entity.Enums.GenderType)model.sex,
                    imageData = model.imageData,
                    imageMimeType = model.imageMimeType,
                    Birthday = new DateTime(model.BirthdayYear, model.BirthdayMonth, model.BirthdayDay),
                    verificationKey = Guid.NewGuid().ToString(),
                    isEmailVerificated = false,
                    isDarkTheme = true
                };
                userModel.role = BLL.Entity.Enums.RoleType.User;

                userService.Create(userModel);

                if (userService.Find(model.login) != null)
                {
                    //FormsAuthentication.SetAuthCookie(model.login, true);

                    string verificationLink = Url.Action("VerificateEmail", "Account", new { key = userModel.verificationKey }, Request.Url.Scheme);
                    var gs = globalSettingsService.Get();
                    var a = new EmailBusiness(gs.smtpIP, gs.smtpPort, gs.smtpMail, gs.smtpPassword);
                    a.SendMail("kinotiki verification email", "Hello " + userModel.login + "! Welcome to KINOTIKI Community! Please use this link to confirm your email: " + verificationLink, userModel.email);

                    return RedirectToAction("WelcomeVerificateEmail", "Account");
                }
            }
            
            return View(model);
        }
        
        public ActionResult WelcomeVerificateEmail()
        {
            return View();
        }

        public ActionResult VerificateEmail(string key)
        {
            var user = userService.FindByVerificationKey(key);
            if (user != null && user.id > 0)
            {
                user.verificationKey = string.Empty;
                user.isEmailVerificated = true;
                userService.Update(user);

                FormsAuthentication.SetAuthCookie(user.login, true);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
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
                var user = userService.Find(model.login, model.password);
                if (user != null && user.id > 0)
                {
                    if(!user.isEmailVerificated)
                    {
                        return RedirectToAction("WelcomeVerificateEmail", "Account");
                    }

                    if(User != null && User.Identity != null && User.Identity.IsAuthenticated)
                        FormsAuthentication.SignOut();
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
            var registerUserModel = mapper.Map<Models.Account.RegisterUser>(user);
            registerUserModel.BirthdayDate = user.Birthday.ToString("yyyy-MM-dd");
            return View(registerUserModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditAccount(Models.Account.RegisterUser user, HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                var _userModel = userService.Find(User.Identity.Name);
                if(_userModel == null || _userModel.id <= 0)
                {
                    ModelState.AddModelError("WrongUser", "Cannot Fetch User.");
                    return View(user);
                }

                if (User.Identity.Name != user.login)
                {
                    var NewUserLogin = userService.Find(user.login);
                    if (NewUserLogin != null && NewUserLogin.id > 0)
                    {
                        ModelState.AddModelError("UserLoginExists", "User With This Login Is Already Exists.");
                        return View(user);
                    }
                }

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
                
                //TODO: Something went wrong with Automapper after last commits
                //var userModel = mapper.Map<BLL.Entity.User>(user);
                var userModel = new BLL.Entity.User()
                {
                    login = user.login,
                    email = user.email,
                    password = user.password,
                    sex = (BLL.Entity.Enums.GenderType)user.sex,
                    imageData = user.imageData,
                    imageMimeType = user.imageMimeType,
                    Birthday = new DateTime(user.BirthdayYear, user.BirthdayMonth, user.BirthdayDay)
                };
                userModel.id = _userModel.id;
                userModel.role = _userModel.role;
                userModel.isDarkTheme = _userModel.isDarkTheme;
                userModel.isEmailVerificated = _userModel.isEmailVerificated;
                userModel.verificationKey = _userModel.verificationKey;

                if (userService.Update(userModel))
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