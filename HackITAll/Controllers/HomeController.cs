using HackITAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackITAll.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            Contact model = new Contact();
            return View(model);
        }
        [HttpPost]
        public ActionResult Contact(Contact model)
        {

            if (ModelState.IsValid)
            {
                using (var context = new DBContexts())
                {
                    context.Contact.Add(model);
                    context.SaveChanges();
                    TempData["msg"] = "<script>alert(' Mesajul a fost trimis cu succes, Va multumim ! ');</script>";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Ne pare rau dar toate campurile sunt obligatorii pentru formularul de contact ! ');</script>";
                return RedirectToAction("Index");
            }

        }

        public ActionResult AboutUs()
        {
            return RedirectToAction("Index");
        }

    }
}