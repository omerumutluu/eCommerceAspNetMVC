using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService = new CartManager(new EfCartDal());
        IProductService _productService = new ProductManager(new EfProductDal());
        IUserService _userService = new UserManager(new EfUserDal());

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int productId)
        {
            if (Session["Email"] != null)
            {
                var sessionUser = _userService.GetByEmail((string)Session["Email"]);
                var product = _productService.GetByProductId(productId);
                if (product != null)
                {
                    Cart cartExistCheck = _cartService.GetCartByUserIdAndProductId(sessionUser.UserId, productId);
                    if (cartExistCheck != null)
                    {
                        cartExistCheck.Count += 1;
                        _cartService.Update(cartExistCheck.CartId, cartExistCheck);
                    }
                    else
                    {
                        Cart cart = new Cart
                        {
                            ProductId = productId,
                            UserId = sessionUser.UserId,
                            Count = 1
                        };
                        _cartService.Add(cart);
                    }
                    return RedirectToAction("GetAll", "Product");
                }
                else
                {
                    return RedirectToAction("GetAll", "Product");
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public ActionResult GetCartDetails(string message="")
        {
            if (Session["Email"] != null)
            {
                var sessionUser = _userService.GetByEmail((string)Session["Email"]);
                var cartValues = _cartService.GetCartByUserId(sessionUser.UserId);
                int totalAmount = 0, price = 0;
                foreach (var cart in cartValues)
                {
                    price = cart.Product.UnitPrice * cart.Count;
                    totalAmount += price; 
                }
                ViewBag.totalAmount = totalAmount;
                ViewBag.shippingCost = 10;
                ViewBag.message = message;
                return View(cartValues);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult MinusOneProduct(int cartId)
        {
            var cartToUpdate = _cartService.GetCartByCartId(cartId);
            if (cartToUpdate.Count > 1)
            {
                cartToUpdate.Count -= 1;
                _cartService.Update(cartId, cartToUpdate);
                return RedirectToAction("GetCartDetails", "Cart");
            }
            else
            {
                _cartService.Delete(cartToUpdate);
                return RedirectToAction("GetCartDetails", "Cart");
            }

        }

        public ActionResult PlusOneProduct(int cartId)
        {
            var cartToUpdate = _cartService.GetCartByCartId(cartId);
            cartToUpdate.Count += 1;
            _cartService.Update(cartId, cartToUpdate);
            return RedirectToAction("GetCartDetails", "Cart");
        }
    }
}