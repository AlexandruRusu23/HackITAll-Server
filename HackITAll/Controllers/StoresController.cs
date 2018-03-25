using HackITAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackITAll.Controllers
{


    public class StoresController : Controller
    {
        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAllForValidation()
        {
            using (var context = new DBContexts())
            {

                var userList = context.Users.Where(x => x.ValidatedUser == false).ToList();

                return View(userList);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ValidateStore(int? id)
        {
            using (var context = new DBContexts())
            {

                var originalUser = context.Users.Where(x => x.UserId == id).FirstOrDefault();
                var user = new Users();
                user = originalUser;
                user.ValidatedUser = true;
                context.Entry(originalUser).CurrentValues.SetValues(user);
                context.SaveChanges();

            }
            return RedirectToAction("GetAllForValidation", "Stores");
        }

        [HttpGet]
        public ActionResult AddProduct(int? id)
        {
            using (var context = new DBContexts())
            {

                var user = context.Users.Where(x => x.UserId == id).FirstOrDefault();

                if(user.ValidatedUser == true)
                {
                    Product prod = new Product();
                    prod.UserId = user.UserId;
                    return View(prod);
                }
                else
                {
                    return View("NotValidUser");
                }

            }
        }

        [HttpPost]
        public ActionResult AddProduct (Product model, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {

                using (var context = new DBContexts())
                {
                    if (image1 != null)
                    {
                        model.Picture = new byte[image1.ContentLength];
                        image1.InputStream.Read(model.Picture, 0, image1.ContentLength);
                    }
                    context.Product.Add(model);
                    context.SaveChanges();
                    TempData["msg"] = "<script>alert('Produs adaugat cu succes ');</script>";
                    return RedirectToAction("AddProduct", "Stores", new { id = model.UserId });
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Ne pare rau dar toate campurile in afara de Descriere sunt obligatorii !! ');</script>";
                return View(model);

            }

        }


    }
}