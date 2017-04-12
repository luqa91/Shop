using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.DAL;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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


        public async Task<ActionResult> Pay()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    Name = user.DataUser.Name,
                    LastName = user.DataUser.Lastname,
                    Address = user.DataUser.Adress,
                    City = user.DataUser.City,
                    PostalCode = user.DataUser.PostCode,
                    Email = user.DataUser.Email,
                    Phone = user.DataUser.Phone,
                };
                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("Pay", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (ModelState.IsValid)
            {
                //pobieramy id uzytkownika aktualnie zalogowanego
                var userId = User.Identity.GetUserId();

                //utworzenie obiektu zamowienia na poidstawie tego co mamy w koszyku
                var newOrder = cartManager.CreateOrder(orderDetails, userId);

                //szczegóły użytkownika - aktualizacja danych
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DataUser);
                await UserManager.UpdateAsync(user);

                //opróżnimy nasz koszyk zakupów
                cartManager.EmptyCart();


                var order = db.Orders.Include("PositionOrder").Include("PositionOrder.Product").SingleOrDefault(o => o.OrderId == newOrder.OrderId);
                ConfirmationOrderEmail email = new ConfirmationOrderEmail();
                email.To = order.Email;
                email.From = "Kaczmareek.lukasz@gmail.com";
                email.Value = order.ValueOrder;
                email.NumberOrder = order.OrderId;
                email.PositionOrder = order.PositionOrder;
               await email.SendAsync();



                return RedirectToAction("ConfirmationOrder");
            }
            else
                return View(orderDetails);

        }
        public ActionResult ConfirmationOrder()
        {
            var name = User.Identity.Name;
            // Logger.Info("Strona koszyk | Potwierdzenie | " + name);
            return View();
        }








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






    }
}