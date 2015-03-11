using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CartPhill.Models;
using CartPhill.ViewModels;

namespace CartPhill.Controllers
{
    public class ShoppingCartController : Controller
    {
        Product storeDB = new Product();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedOpp = storeDB.Hoards
                .Single(opps => opps.Id == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);//dets for adding
            cart.AddToCart(addedOpp);//opp added to cart
            return RedirectToAction("Index");//return for more shopping
        }
        // Here comes the AJAX- Remove from cart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string ProductName = storeDB.Carts.Single(item => item.RecordId == id).product.Name;
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(ProductName) +
                          " has been thrown back into the void from which it came",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //GET; /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}