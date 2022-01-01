using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    [Authorize(Roles = "customer")]
    public class UserController : Controller
    {
        IUserService _userService = new UserManager(new EfUserDal());
        ICustomerAccountService _customerAccountService = new CustomerAccountManager(new EfCustomerAccountDal());

        public ActionResult GetProfile()
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

            return View(customerAccountInfo);
        }

        [HttpGet]
        public ActionResult EditProfile()
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
            return View(customerAccountInfo);
        }

        [HttpPost]
        public ActionResult EditProfile(CustomerAccount customerAccount)
        {
            CustomerAccountValidator validator = new CustomerAccountValidator();
            ValidationResult result = validator.Validate(customerAccount);
            if (result.IsValid)
            {
                var customerToUpdate = _customerAccountService.GetByCustomerAccountId(customerAccount.CustomerAccountId);
                customerToUpdate.FirstName = customerAccount.FirstName;
                customerToUpdate.LastName = customerAccount.LastName;
                customerToUpdate.PhoneNumber = customerAccount.PhoneNumber;
                customerToUpdate.Address = customerAccount.Address;
                _customerAccountService.Update(customerToUpdate);
                return RedirectToAction("GetProfile", "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }
    }
}