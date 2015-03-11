using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using CartPhill.Models;

namespace CartPhill.Models
{
    public partial class ShoppingCart
    {
        Product storeDB = new Product();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);

        }

        public void AddToCart(OPP opp)
        {
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.ProductId == opp.Id);
            if (cartItem == null)
            {
                cartItem = new Cart()
                {
                    ProductId = opp.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                        && cart.RecordId == id); //Gets the cart
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {

                    storeDB.Carts.Remove(cartItem);

                }
                storeDB.SaveChanges();
             }
             return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            storeDB.SaveChanges();

        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //Will get the count of each item in the cart
            int? count = (from cartItems in storeDB.Carts
                where cartItems.CartId == ShoppingCartId
                select (int?) cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()

        {
            //opps price times count and all summed for cart totoal
            decimal? total = (from cartItems in storeDB.Carts
                where cartItems.CartId == ShoppingCartId
                select (int?) cartItems.Count*
                       cartItems.product.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            //iieeett-itteerrating over the cart items adding the order details)
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Price = item.product.Price,
                    Quanity = item.Count
  
                };
                orderTotal += (item.Count*item.product.Price);
                storeDB.OrderDetails.Add(orderDetail);
            }
            order.Total = orderTotal;
            storeDB.SaveChanges();
            EmptyCart();
            return order.OrderId;//returns the id as confirmation number
        }
//allowing access to cookies using HTTPContextBase
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {//generating new random GUID using system.Guid c;ass
                    Guid tempCartId = Guid.NewGuid();
                    //send te,pCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        //when the user logs in migrate their cart to their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}