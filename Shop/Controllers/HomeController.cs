using MvcSiteMapProvider.Caching;
using NLog;
using NLog.Fluent;
using Shop.DAL;
using Shop.Infrastructure;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            
            Log.Warn("This is a warning message");
            Log.Error("This is an error message");
            
            Log.Info("Jestes na stronie głównej");

            ICacheProvider cache = new DefaultCacheProvider();

            List<Category> categories;
            if (cache.IsSet(Consts.CategoriesCacheKey))
            {
                categories = cache.Get(Consts.CategoriesCacheKey) as List<Category>;
            }
            else
            {

                categories = db.Categories.ToList();
                cache.Set(Consts.CategoriesCacheKey, categories, 60);
            }





            List<Product> news;
            if(cache.IsSet(Consts.NewsCacheKey))
            {
                news = cache.Get(Consts.NewsCacheKey) as List<Product>;
            }
            else
            {
                news = db.Products.Where(a => !a.Hidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
                cache.Set(Consts.NewsCacheKey, news, 60);
            }

            List<Product> bestsellers;
            if (cache.IsSet(Consts.BestsellersCacheKey))
            {
                bestsellers = cache.Get(Consts.BestsellersCacheKey) as List<Product>;
            }
            else
            {

                bestsellers = db.Products.Where(a => !a.Hidden && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellersCacheKey, bestsellers, 60);
            }



            var vm = new HomeViewModel()
            {
                Categories = categories,
                News = news,
                Bestsellers=bestsellers,
                
            };

            return View(vm);
        }
        
        public ActionResult StaticSite(string name)
        {


            return View(name);

        }






        public ActionResult Index2()
        {

            
            return View();
        }







    }
}