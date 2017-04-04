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

        public ActionResult List(string nameCategory)
        {
            var category = db.Categories.Include("Products").Where(k => k.NameCategory.ToLower() == nameCategory.ToLower()).Single();
            var products = category.Products.ToList();


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

        

    }
}