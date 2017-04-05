using Shop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {

        private ProductsContext db = new ProductsContext();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string nameCategory, string searchQuery = null)
        {
            var category = db.Categories.Include("Products").Where(k => k.NameCategory.ToLower() == nameCategory.ToLower()).Single();
            var products = category.Products.Where(a => (searchQuery == null ||
                           a.Title.ToLower().Contains(searchQuery.ToLower()) ||
                           a.Company.ToLower().Contains(searchQuery.ToLower())) && !a.Hidden);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductsList", products);
            }

            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }




        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var menu = db.Categories.ToList();
            return PartialView("_CategoryMenu", menu);
        }



        public ActionResult ProductsHints(string term)
        {
            var products = db.Products.Where(a => !a.Hidden && a.Title.ToLower().Contains(term.ToLower()))
                .Take(5).Select(a => new { label = a.Title });
            return Json(products, JsonRequestBehavior.AllowGet);

        }



    }
}