using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using Entity.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        IAuthService _authService = new AuthManager(new EfUserDal());
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string message = " ")
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserForLoginDto userForLoginDto, string message = " ")
        {
            var userInfo = _authService.Login(userForLoginDto);
            if (userInfo != null)
            {
                FormsAuthentication.SetAuthCookie(userInfo.Email, false);
                Session["Email"] = userInfo.Email;
                return RedirectToAction("GetAll", "Product");
            }
            else
            {
                return RedirectToAction("Login","Auth",new { message = "Email ve ya parolanızı kontrol ediniz." });
            }
        }

        [HttpGet]
        public ActionResult RegisterAsCustomer(string message = " ")
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAsCustomer(CustomerAccountForRegisterDto customerAccountForRegisterDto, string message = " ")
        {
            CustomerAccountForRegisterDtoValidator validator = new CustomerAccountForRegisterDtoValidator();
            ValidationResult results = validator.Validate(customerAccountForRegisterDto);
            if (results.IsValid)
            {
                _authService.CustomerAccountRegister(customerAccountForRegisterDto);
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RegisterAsCorporate(string message = " ")
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAsCorporate(CorporateAccountForRegisterDto corporateAccountForRegisterDto, string message = " ")
        {
            CorporateAccountForRegisterDtoValidator validator = new CorporateAccountForRegisterDtoValidator();
            ValidationResult results = validator.Validate(corporateAccountForRegisterDto);
            if (results.IsValid)
            {
                _authService.CorporateAccountRegister(corporateAccountForRegisterDto);
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("GetAll", "Product");
        }
    }
}