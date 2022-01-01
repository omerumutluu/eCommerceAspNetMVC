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
using Business.ValidationRules.FluentValidation;
using FluentValidation.Results;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IUserService _userService = new UserManager(new EfUserDal());
        IAdminAccountService _adminAccountService = new AdminAccountManager(new EfAdminAccountDal());
        ICategoryService _categoryService = new CategoryManager(new EfCategoryDal());
        ICorporateAccountService _corporateAccountService = new CorporateAccountManager(new EfCorporateAccountDal());
        ICustomerAccountService _customerAccountService = new CustomerAccountManager(new EfCustomerAccountDal());
        IProductService _productService = new ProductManager(new EfProductDal());
        IBrandService _brandService = new BrandManager(new EfBrandDal());

        public ActionResult Home()
        {
            return RedirectToAction("GetAdmins","Admin");
        }

        [HttpGet]
        public ActionResult GetAdmins(int page = 1)
        {
            var admins = _adminAccountService.GetAllAdminAccounts().ToPagedList(page, 10);
            return View(admins);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(AdminAccount adminAccount, string userMail, string userPassword)
        {
            AdminAccountValidator adminAccountValidator = new AdminAccountValidator();
            ValidationResult validationResult = adminAccountValidator.Validate(adminAccount);

            if (validationResult.IsValid)
            {
                User newUser = new User { Email = userMail, Password = userPassword, Status = true };
                _userService.Add(newUser);
                User newUserInfo = _userService.GetByEmail(userMail);
                adminAccount.UserId = newUserInfo.UserId;
                adminAccount.Role = "admin";
                adminAccount.Status = true;
                _adminAccountService.Add(adminAccount);
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetCorporateCustomers(int page = 1)
        {
            var corporateCustomers = _corporateAccountService.GetAll().ToPagedList(page, 10);
            return View(corporateCustomers);
        }

        [HttpGet]
        public ActionResult GetBrands(int page = 1)
        {
            var brands = _brandService.GetAll().ToPagedList(page, 10);
            return View(brands);
        }

        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(Brand brand)
        {
            if (brand.BrandName != null)
            {
                _brandService.Add(brand);
                return RedirectToAction("GetBrands", "Admin");
            }
            else
            {
                return RedirectToAction("AddBrand", "Admin");
            }
        }

        [HttpGet]
        public ActionResult EditBrand(int brandId)
        {
            Brand brand = _brandService.GetByBrandId(brandId);
            return View(brand);
        }

        [HttpPost]
        public ActionResult EditBrand(Brand brand)
        {
            if (brand.BrandName != null)
            {
                _brandService.Update(brand);
                return RedirectToAction("GetBrands", "Admin");
            }
            else
            {
                return RedirectToAction("AddBrand", "Admin");
            }
        }

        [HttpGet]
        public ActionResult DeleteBrand(int brandId)
        {
            Brand brand = _brandService.GetByBrandId(brandId);
            _brandService.Delete(brand);
            return RedirectToAction("GetBrands", "Admin");
        }

        [HttpGet]
        public ActionResult GetCustomerAccounts(int page = 1)
        {
            var customerAccounts = _customerAccountService.GetAll().ToPagedList(page, 10);
            return View(customerAccounts);
        }

        [HttpGet]
        public ActionResult GetCategories(int page = 1)
        {
            var categories = _categoryService.GetAll().ToPagedList(page, 10);
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (category.CategoryName != null)
            {
                _categoryService.Add(category);
                return RedirectToAction("GetCategories", "Admin");
            }
            else
            {
                return RedirectToAction("AddCategory", "Admin");
            }
        }

        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            Category category = _categoryService.GetByCategoryId(categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (category.CategoryName != null)
            {
                _categoryService.Update(category);
                return RedirectToAction("GetCategories", "Admin");
            }
            else
            {
                return RedirectToAction("AddCategory", "Admin");
            }
        }

        [HttpGet]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category category = _categoryService.GetByCategoryId(categoryId);
            _categoryService.Delete(category);
            return RedirectToAction("GetCategories", "Admin");
        }

        [HttpGet]
        public ActionResult GetProducts(int page = 1)
        {
            var products = _productService.GetAll().ToPagedList(page,10);
            return View(products);
        }
    }
}