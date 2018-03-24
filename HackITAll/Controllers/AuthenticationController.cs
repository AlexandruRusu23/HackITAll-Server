using HackITAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HackITAll.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            Users model = new Users()
            {
                ReturnUrl = returnUrl
            };
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Users model)
        {
            Users findAdmin = new Users();

            using (var context = new DBContexts())
            {
                findAdmin = context.Users.Where(x => x.UserName == "admin" && x.Password == "admin" && x.Role == "admin").FirstOrDefault();
            }
            if (model.UserName == findAdmin.UserName && model.Password == findAdmin.Password)
            {
                try
                {
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, findAdmin.UserName),
                    new Claim(ClaimTypes.Role, findAdmin.Role),
                    new Claim(ClaimTypes.UserData, findAdmin.UserId.ToString()),
                    }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return Redirect(GetRedirectUrl(model.ReturnUrl));
                }
                catch (Exception) { }
            }
            else
            {
                using (var context = new DBContexts())
                {
                    var findUser = context.Users.ToList().Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
                    if (findUser != null)
                    {
                        try
                        {
                            var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, model.UserName),
                            new Claim(ClaimTypes.Role, "user"),
                            new Claim(ClaimTypes.UserData, findUser.UserId.ToString())
                            }, "ApplicationCookie");

                            var ctx = Request.GetOwinContext();
                            var authManager = ctx.Authentication;

                            authManager.SignIn(identity);

                            return Redirect(GetRedirectUrl(model.ReturnUrl));
                        }
                        catch (Exception) { }
                    }
                }
            }

            //auth fail
            ModelState.AddModelError("", "Invalid username or password");
            return RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Register()
        {
            Users model = new Users();
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(Users model)
        {

            var isValid = true;
            model.ValidatedUser = false;

            if (!ModelState.IsValid)
            {
                isValid = false;
            }
            using (var context = new DBContexts())
            {

                model.Role = "client";
                var userList = context.Users.ToList();
                bool dublura = false;

                foreach (var user in userList)
                {
                    if (user.UserName == model.UserName)
                    {
                        dublura = true;
                    }
                }

                if (dublura == false && isValid == true)
                {
                    context.Users.Add(model);
                    context.SaveChanges();
                }
                else if (dublura == true)
                {
                    TempData["msg"] = "<script>alert('Acest Username este deja folosit! ');</script>";
                }
                else if (isValid == false)
                {
                    TempData["msg"] = "<script>alert('Toate campurile sunt obligatorii !! ');</script>";
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            Users currentUserInfo = new Users();

            if (id != null)
            {
                var userId = Int32.Parse(id);

                using (var context = new DBContexts())
                {
                    currentUserInfo = context.Users.Find(userId);
                }

                return View(currentUserInfo);

            }
            else
                return View(currentUserInfo);

        }

        [HttpPost]
        public ActionResult Update(Users model)
        {
            using (var context = new DBContexts())
            {
                var userList = context.Users.Where(x => x.UserId != model.UserId).ToList();
                bool dublura = false;

                foreach (var user in userList)
                {
                    if (user.UserName == model.UserName)
                    {
                        dublura = true;
                    }
                }

                if (dublura == false)
                {
                    var original = context.Users.Find(model.UserId);
                    context.Entry(original).CurrentValues.SetValues(model);
                    context.SaveChanges();
                }
                else
                {
                    TempData["msg"] = "<script>alert('Acest Username este deja folosit! ');</script>";
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}