using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class CartManager
    {
        private ProductsContext db;
        private ISessionManager session;
        public CartManager(ISessionManager session, ProductsContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<PositionCart> DownloadCart()
        {

            List<PositionCart> cart;
            if (session.Get<List<PositionCart>>(Consts.CartSessionKey) == null)
            {
                cart = new List<PositionCart>();
            }
            else
            {
                cart = session.Get<List<PositionCart>>(Consts.CartSessionKey) as List<PositionCart>;
            }


            return cart;
        }

        public void AddToCart(int productId)
        {
            var cart = DownloadCart();
            var positionCart = cart.Find(k => k.Product.ProductId == productId);
            if (positionCart != null)
                positionCart.Quantity++;
            else
            {
                var productToAdd= db.Products.Where(k => k.ProductId== productId).SingleOrDefault();
                if (productToAdd!= null)
                {
                    var newPositionCart = new PositionCart()
                    {
                        Product = productToAdd,
                        Quantity = 1,
                        Value = productToAdd.PriceProduct
                    };
                    cart.Add(newPositionCart);
                }
            }

            session.Set(Consts.CartSessionKey, cart);

        }

        public int DeleteFromCart(int productId)
        {
            var cart = DownloadCart();
            var positionCart = cart.Find(k => k.Product.ProductId== productId);

            if (positionCart!= null)
            {
                if (positionCart.Quantity> 1)
                {
                    positionCart.Quantity--;
                    return positionCart.Quantity;
                }
                else
                {
                    cart.Remove(positionCart);
                }
            }

            return 0;
        }

        public decimal DownloadValueCart()
        {
            var cart = DownloadCart();
            return cart.Sum(k => (k.Quantity* k.Product.PriceProduct));
        }


        public int DownloadQuantityPositionCart()
        {
            var cart = DownloadCart();
            int quantity= cart.Sum(k => k.Quantity);
            return quantity;
        }

        public Order CreateOrder (Order newOrder, string userId)
        {
            var cart = DownloadCart();
            newOrder.DateAdded = DateTime.Now;
            newOrder.UserId = userId;

            db.Orders.Add(newOrder);

            if (newOrder.PositionOrder == null)
                newOrder.PositionOrder = new List<PositionOrder>();

            decimal cartValue = 0;

            foreach (var cartElement in cart)
            {
                var newPositionOrder = new PositionOrder()
                {
                    ProductId = cartElement.Product.ProductId,
                    Quantity = cartElement.Quantity,
                    PricePurchase = cartElement.Product.PriceProduct
                };
                cartValue += (cartElement.Quantity * cartElement.Product.PriceProduct);
                newOrder.PositionOrder.Add(newPositionOrder);
            }

            newOrder.ValueOrder = cartValue;
            db.SaveChanges();

            return newOrder;

        }
        public void EmptyCart()
        {
            session.Set<List<PositionCart>>(Consts.CartSessionKey, null);
        }
    }
}