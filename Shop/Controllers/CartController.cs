using Shop.DAL;
using Shop.Infrastructure;
using Shop.ViewModels;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CartController : Controller
    {

        private CartManager cartManager;
        private ISessionManager sessionManager { get; set; }
        private ProductsContext db;


        public CartController()
        {


            this.db = new ProductsContext();

            //  this.mailService = mailService;
            this.sessionManager = new SessionManager();
            this.cartManager = new CartManager(sessionManager, db);
            //
        }
        

        // GET: Cart
        public ActionResult Index()
        {
            var positionCart = cartManager.DownloadCart();
            var priceTotal = cartManager.DownloadValueCart();
            CartViewModel cartVM = new CartViewModel()
            {
                PositionCart = positionCart,
                PriceTotal = priceTotal
            };


            return View(cartVM);
        }


        public ActionResult BuyNow(int id)
        {
            cartManager.AddToCart(id);
            return RedirectToAction("Index");
        }

        public int DownloadQuantityElements()

        {
            return cartManager.DownloadQuantityPositionCart();


        }

        public ActionResult DeleteFromCart(int productId2)
        {


            int quantityPosition = cartManager.DeleteFromCart(productId2);
            int quantityPositionCart = cartManager.DownloadQuantityPositionCart();
            decimal valueCart = cartManager.DownloadValueCart();

            var resume = new CartDeleteViewModel
            {
                IdPositionDeleting = productId2,
                QuantityPositionDeleting = quantityPosition,
                CartPriceTotal= valueCart,
                CartQuantityPosition= quantityPositionCart,

            };
            return Json(resume);
        }


        




    }
}