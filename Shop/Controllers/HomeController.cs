using Shop.DAL;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private ProductsContext db = new ProductsContext();
        public ActionResult Index()
        {

            var category = db.Categories.ToList();
            var news = db.Products.Where(a => !a.Hidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
            var bestseller = db.Products.Where(a => !a.Hidden && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();


            var vm = new HomeViewModel()
            {
                Categories = category,
                News = news,
                Bestsellers=bestseller,
                
            };

            return View(vm);
        }
        
        public ActionResult StaticSite(string name)
        {


            return View(name);

        }



    }
}