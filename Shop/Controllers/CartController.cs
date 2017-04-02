using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BuyNow(string id)
        {
            return View();
        }


    }
}