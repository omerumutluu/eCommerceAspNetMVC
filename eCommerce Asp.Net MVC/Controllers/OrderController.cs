using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService = new OrderManager(new EfOrderDal());
        ICartService _cartService = new CartManager(new EfCartDal());
        IProductService _productService = new ProductManager(new EfProductDal());
        IUserService _userService = new UserManager(new EfUserDal());
        ICustomerAccountService _customerAccountService = new CustomerAccountManager(new EfCustomerAccountDal());

        public ActionResult CreateOrder()
        {
            User userInfo;
            CustomerAccount customerAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                customerAccountInfo = _customerAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var carts = _cartService.GetCartByUserId(userInfo.UserId);
            List<Order> orders = new List<Order>();
            Product productToUpdate;
            foreach (var cart in carts)
            {
                productToUpdate = _productService.GetByProductId(cart.ProductId);
                if (!(productToUpdate.UnitsInStock < cart.Count))
                {
                    productToUpdate.UnitsInStock -= cart.Count;
                    _productService.Update(productToUpdate.ProductId, productToUpdate);
                    orders.Add(new Order
                    {
                        CustomerAccountId = customerAccountInfo.CustomerAccountId,
                        ProductId = cart.ProductId,
                        OrderStatus = "Onay Bekleniyor",
                        Count = cart.Count
                    });
                }
                else
                {
                    return RedirectToAction("GetCartDetails", "Cart", new { message = "Sipariş ettiğiniz ürün adeti mevcut olan ürün stoğundan fazla olamaz. Lütfen sipariş ettiğiniz ürün sayılarını kontrol ediniz." });
                }
            }
            foreach (var order in orders)
            {
                _orderService.Add(order);
            }
            foreach (var cart in carts)
            {
                _cartService.Delete(cart);
            }
            return RedirectToAction("GetAll", "Product");
        }

        [HttpGet]
        public ActionResult ChangeStatusAsPreparing(int orderId)
        {
            User userInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var orderToUpdate = _orderService.GetByOrderId(orderId);
            orderToUpdate.OrderStatus = "Hazırlanıyor";
            _orderService.Update(orderToUpdate);
            return RedirectToAction("GetAllOrder", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult ChangeStatusAsInShipping(int orderId)
        {
            User userInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var orderToUpdate = _orderService.GetByOrderId(orderId);
            orderToUpdate.OrderStatus = "Kargoda";
            _orderService.Update(orderToUpdate);
            return RedirectToAction("GetAllOrder", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult ChangeStatusAsCompleted(int orderId)
        {
            User userInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var orderToUpdate = _orderService.GetByOrderId(orderId);
            orderToUpdate.OrderStatus = "Tamamlandı";
            _orderService.Update(orderToUpdate);
            return RedirectToAction("GetAllOrder", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult ChangeStatusAsCanceled(int orderId)
        {
            User userInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var orderToUpdate = _orderService.GetByOrderId(orderId);
            orderToUpdate.OrderStatus = "İptal Edildi";
            _orderService.Update(orderToUpdate);
            return RedirectToAction("GetAllOrder", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult GetOrdersByCustomer(int page=1)
        {
            User userInfo;
            CustomerAccount customerAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                customerAccountInfo = _customerAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }

            var orders = _orderService.GetByCustomerAccountId(customerAccountInfo.CustomerAccountId).ToPagedList(page,6);
            return View(orders);
        }
    }
}