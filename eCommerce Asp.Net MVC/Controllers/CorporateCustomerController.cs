using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    [Authorize(Roles = "corporate")]
    public class CorporateCustomerController : Controller
    {
        IProductService _productService = new ProductManager(new EfProductDal());
        ICategoryService _categoryService = new CategoryManager(new EfCategoryDal());
        IBrandService _brandService = new BrandManager(new EfBrandDal());
        IUserService _userService = new UserManager(new EfUserDal());
        ICorporateAccountService _corporateAccountService = new CorporateAccountManager(new EfCorporateAccountDal());
        IOrderService _orderService = new OrderManager(new EfOrderDal());
        ICommentService _commentService = new CommentManager(new EfCommentDal());

        [HttpGet]
        public ActionResult Home()
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);

            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.waitingConfirmOrder = _orderService.GetWaitingConfirmOrdersByCorporateCustomer(corporateAccountInfo.CorporateAccountId).Count;
            ViewBag.preparingOrder = _orderService.GetPreparingOrdersByCorporateCustomer(corporateAccountInfo.CorporateAccountId).Count;
            ViewBag.onShippingOrder = _orderService.GetOnShippingOrdersByCorporateCustomer(corporateAccountInfo.CorporateAccountId).Count;
            ViewBag.compeletedOrder = _orderService.GetCompletedOrdersByCorporateCustomer(corporateAccountInfo.CorporateAccountId).Count;
            ViewBag.cancelledOrder = _orderService.GetCancelledOrdersByCorporateCustomer(corporateAccountInfo.CorporateAccountId).Count;
            ViewBag.totalProductCountForCorporateCustomer = _productService.GetByCorporateCustomerId(corporateAccountInfo.CorporateAccountId).Count();
            ViewBag.countOfCommentToCorporateCustomerProduct = _commentService.GetCommentsByCorporateCustomerId(corporateAccountInfo.CorporateAccountId).Count;
            return View(corporateAccountInfo);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> categoryValues = (from x in _categoryService.GetAll() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> brandValues = (from x in _brandService.GetAll() select new SelectListItem { Text = x.BrandName, Value = x.BrandId.ToString() }).ToList();

            ViewBag.CategoryId = categoryValues;
            ViewBag.BrandId = brandValues;

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, string categoryId, string brandId)
        {
            List<SelectListItem> categoryValues = (from x in _categoryService.GetAll() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> brandValues = (from x in _brandService.GetAll() select new SelectListItem { Text = x.BrandName, Value = x.BrandId.ToString() }).ToList();

            ViewBag.CategoryId = categoryValues;
            ViewBag.BrandId = brandValues;

            if (Request.Files.Count > 0)
            {
                var extension = Path.GetExtension(Request.Files[0].FileName);
                var newFileName = Guid.NewGuid() + extension;
                var path = "/Images/ProductThumbnails/" + newFileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                product.ProductThumbnail = "/Images/ProductThumbnails/" + newFileName;
            }

            ProductValidator productValidator = new ProductValidator();
            ValidationResult validationResult = productValidator.Validate(product);
            if (validationResult.IsValid)
            {
                var userInfo = _userService.GetByEmail((string)Session["Email"]);
                var corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
                product.CategoryId = Int32.Parse(categoryId);
                product.BrandId = Int32.Parse(brandId);
                product.CorporateAccountId = corporateAccountInfo.CorporateAccountId;
                _productService.Add(product);
                return RedirectToAction("Home", "CorporateCustomer");
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
        public ActionResult DeleteProduct(int productId)
        {
            var product = _productService.GetByProductId(productId);
            _productService.Delete(product);
            return RedirectToAction("Home", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {

            ViewBag.categories = _categoryService.GetAll();
            ViewBag.brands = _brandService.GetAll();

            var productValue = _productService.GetByProductId(productId);

            ViewBag.categoryId = productValue.CategoryId.ToString();
            ViewBag.brandId = productValue.BrandId.ToString();

            return View(productValue);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string categoryId, string brandId)
        {
            var productBackup = _productService.GetByProductId(product.ProductId);

            ViewBag.categories = _categoryService.GetAll();
            ViewBag.brands = _brandService.GetAll();

            if (Request.Files.Count > 0)
            {
                var extension = Path.GetExtension(Request.Files[0].FileName);
                var newFileName = Guid.NewGuid() + extension;
                var path = "/Images/ProductThumbnails/" + newFileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                product.ProductThumbnail = "/Images/ProductThumbnails/" + newFileName;
            }
            else
            {
                product.ProductThumbnail = productBackup.ProductThumbnail;
            }

            ProductValidator productValidator = new ProductValidator();
            ValidationResult validationResult = productValidator.Validate(product);

            if (validationResult.IsValid)
            {
                User userInfo;
                CorporateAccount corporateAccountInfo;
                try
                {
                    userInfo = _userService.GetByEmail((string)Session["Email"]);
                    corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
                }
                catch
                {
                    return RedirectToAction("Login", "Auth");
                }
                productBackup.CategoryId = Int32.Parse(categoryId);
                productBackup.BrandId = Int32.Parse(brandId);
                productBackup.ProductName = product.ProductName;
                productBackup.UnitPrice = product.UnitPrice;
                productBackup.UnitsInStock = product.UnitsInStock;
                product.CorporateAccountId = corporateAccountInfo.CorporateAccountId;
                //_productService.Add(product);
                //_productService.Delete(productBackup);
                _productService.Update(productBackup.ProductId, productBackup);
                return RedirectToAction("Home", "CorporateCustomer");
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return RedirectToAction("Home", "CorporateCustomer");
        }

        [HttpGet]
        public ActionResult GetProductsByCorporateCustomer(int page=1)
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var products = _productService.GetByCorporateCustomerId(corporateAccountInfo.CorporateAccountId).ToPagedList(page,10);
            return View(products);

        }

        public ActionResult ProfileDetails()
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(corporateAccountInfo);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(corporateAccountInfo);
        }

        [HttpPost]
        public ActionResult EditProfile(CorporateAccount corporateAccount)
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            CorporateAccountValidator corporateAccountValidator = new CorporateAccountValidator();
            ValidationResult corporateAccountResult = corporateAccountValidator.Validate(corporateAccount);

            if (corporateAccountResult.IsValid)
            {
                corporateAccountInfo.CorporateName = corporateAccount.CorporateName;
                corporateAccountInfo.WebSite = corporateAccount.WebSite;
                corporateAccountInfo.PhoneNumber = corporateAccount.PhoneNumber;
                _corporateAccountService.Update(corporateAccountInfo);
                return RedirectToAction("ProfileDetails", "CorporateCustomer");
            }
            else
            {
                foreach (var error in corporateAccountResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string passwordRepeat)
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
            ViewBag.message = "";
            if (password == passwordRepeat)
            {
                if (password.Length < 8 || password.Length > 30)
                {
                    ViewBag.message = "Girdiğiniz parola 8 karekter ile 30 karekter aralığında olabilir.";
                }
                else
                {
                    userInfo.Password = password;
                    _userService.Update(userInfo);
                    return RedirectToAction("Home", "CorporateCustomer");
                }
            }
            else
            {
                ViewBag.message = "Girdiğiniz parolalar eşleşmemektedir.";
            }
            return View();
        }

        public ActionResult GetAllOrder(int page = 1)
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }

            var orders = _orderService.GetByCorporateAccountId(corporateAccountInfo.CorporateAccountId).ToPagedList(page,10);
            return View(orders);
        }

        public ActionResult GetCommentByCorporateCustomer(int page = 1)
        {
            User userInfo;
            CorporateAccount corporateAccountInfo;
            try
            {
                userInfo = _userService.GetByEmail((string)Session["Email"]);
                corporateAccountInfo = _corporateAccountService.GetByUserId(userInfo.UserId);

            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }
            var comments = _commentService.GetCommentsByCorporateCustomerId(corporateAccountInfo.CorporateAccountId).ToPagedList(page, 15);
            return View(comments);
        }
    }
}