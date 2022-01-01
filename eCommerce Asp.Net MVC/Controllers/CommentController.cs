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
    public class CommentController : Controller
    {
        IUserService _userService = new UserManager(new EfUserDal());
        ICustomerAccountService _customerAccountService = new CustomerAccountManager(new EfCustomerAccountDal());
        ICommentService _commentService = new CommentManager(new EfCommentDal());
        public ActionResult GetCommentsByUser(int page = 1)
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
            var comments = _commentService.GetByUserId(userInfo.UserId).ToPagedList(page, 5);
            return View(comments);
        }

        [HttpPost]
        public ActionResult AddComment(Product product, string commentTitle, string commentLevel, string commentDetail)
        {
            User userInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
            }
            catch
            {
                return RedirectToAction("Auth", "Login");
            }
            Comment comentToAdd = new Comment
            {
                UserId = userInfo.UserId,
                ProductId = product.ProductId,
                CommentTitle = commentDetail,
                CommentLevel = Int32.Parse(commentLevel),
                CommentDetail = commentDetail
            };
            _commentService.Add(comentToAdd);
            return RedirectToAction("GetProductDetails", "Product", new { id = product.ProductId });
        }

        [HttpGet]
        public ActionResult GetCommentByUser(int page = 1)
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

            var comments = _commentService.GetByUserId(userInfo.UserId).ToPagedList(page, 8);
            return View(comments);
        }
    }
}