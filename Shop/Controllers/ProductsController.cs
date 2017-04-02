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
            return View();
        }

        public ActionResult MoreInfo(string id)
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var menu = db.Categories.ToList();
            return PartialView("_CategoryMenu", menu);
        }

        

    }
}