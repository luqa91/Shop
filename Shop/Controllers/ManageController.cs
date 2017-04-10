using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.DAL;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Shop.ViewModels.ManageViewModels;

namespace Shop.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
            public enum ManageMessageId
            {
                ChangePasswordSuccess,
                Error
            }

            private ProductsContext db = new ProductsContext();

            private ApplicationUserManager _userManager;
            public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }

            private IAuthenticationManager AuthenticationManager
            {
                get
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
            }



            //
            // GET: /Manage/Index
            public async Task<ActionResult> Index(ManageMessageId? message)
            {
                if (TempData["ViewData"] != null)
                {
                    ViewData = (ViewDataDictionary)TempData["ViewData"];
                }

                if (User.IsInRole("Admin"))
                    ViewBag.UserIsAdmin = true;
                else
                    ViewBag.UserIsAdmin = false;

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (User == null)
                {
                    return View("Error");
                }

                var model = new ManageCredentialsViewModel
                {
                    Message = message,
                    DataUser = user.DataUser
                };

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DataUser")]DataUser dataUser)
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    user.DataUser = dataUser;
                    var result = await UserManager.UpdateAsync(user);

                    AddErrors(result);
                }

                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);

                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                var message = ManageMessageId.ChangePasswordSuccess;
                return RedirectToAction("Index", new { Message = message });
            }


            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("password-error", error);
                }
            }

            private async Task SignInAsync(ApplicationUser user, bool isPersistent)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
            }
        

        public ActionResult OrderList ()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Order> OrderListUsers;

            //Dla administratora szwracamy wszystkie zamówienia
            if(isAdmin)
            {
                OrderListUsers = db.Orders.Include("PositionOrder").OrderByDescending(o => o.DateAdded).ToArray();

            }
            else
            {
                var userId = User.Identity.GetUserId();
                OrderListUsers = db.Orders.Where(o=>o.UserId==userId).Include("PositionOrder").OrderByDescending(o => o.DateAdded).ToArray();
            }
            return View(OrderListUsers);
            
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public StatusOrder ChangeStatusOrder(Order order)
        {
            Order ordertomodification = db.Orders.Find(order.OrderId);
            ordertomodification.StatusOrder = order.StatusOrder;
            db.SaveChanges();
            return order.StatusOrder;

        }


        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(int? productId, bool? confirmation)
        {
            Product product;
            if (productId.HasValue)
            {
                ViewBag.EditMode = true;
                product = db.Products.Find(productId);
            }
            else
            {
                ViewBag.EditMode = false;
                product = new Product();
            }

            var result = new EditProductViewModel();
            result.Category = db.Categories.ToList();
            result.Product = product;
            result.Confirmation = confirmation;

            return View(result);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(EditProductViewModel model, HttpPostedFileBase file)
        {
            if (model.Product.ProductId > 0)
            {
                // modyfikacja kursu
                db.Entry(model.Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddProduct", new { confirmation = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.PicturesFolderRelative), filename);
                        file.SaveAs(path);

                        model.Product.NameFileImage = filename;
                        model.Product.DateAdded = DateTime.Now;

                        db.Entry(model.Product).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("AddProduct", new { confirmation = true });
                    }
                    else
                    {
                        var category = db.Categories.ToList();
                        model.Category = category;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var categories = db.Categories.ToList();
                    model.Category = categories;
                    return View(model);
                }
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult HideProduct(int productId)
        {
            var album = db.Products.Find(productId);
            album.Hidden = true;
            db.SaveChanges();


            return RedirectToAction("AddProduct", new { confirmation = true });

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShowProduct(int productId)
        {
            var album = db.Products.Find(productId);
            album.Hidden = false;
            db.SaveChanges();


            return RedirectToAction("AddProduct", new { confirmation = true });

        }





    }
}