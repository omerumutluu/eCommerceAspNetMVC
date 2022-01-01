using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using Entity.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using eCommerce_Asp.Net_MVC.Models;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        IProductService _productService = new ProductManager(new EfProductDal());
        ICategoryService _categoryService = new CategoryManager(new EfCategoryDal());
        IBrandService _brandService = new BrandManager(new EfBrandDal());
        ICartService _cartService = new CartManager(new EfCartDal());
        ICommentService _commentService = new CommentManager(new EfCommentDal());

        [HttpGet]
        public ActionResult GetAll(int page = 1)
        {
            var products = _productService.GetAllIsHaveStock().ToPagedList(page, 9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult GetAllByNameSearch(string nameSearch,int page=1)
        {
            var products = _productService.GetAllByNameSearch(nameSearch.ToLower()).ToPagedList(page,9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult GetByCategoryId(int categoryId,int page = 1)
        {
            var products = _productService.GetByCategoryId(categoryId).ToPagedList(page,9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult GetProductDetails(int id)
        {
            var product = _productService.GetByProductId(id);
            ViewBag.comments = _commentService.GetByProductId(id);
            return View(product);
        }
        
        [HttpGet]
        public ActionResult GetAllByPrice(int minimumValue, int maximumValue, int page=1)
        {
            var product = _productService.GetAllByPrice(minimumValue, maximumValue).ToPagedList(page,9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(product);
        }

        [HttpGet]
        public ActionResult GetAllByPriceAsc(int page = 1)
        {
            var product = _productService.GetAllByPriceAsc().ToPagedList(page, 9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(product);
        }

        [HttpGet]
        public ActionResult GetAllByPriceDesc(int page = 1)
        {
            var product = _productService.GetAllByPriceDesc().ToPagedList(page, 9);
            ViewBag.categories = (from x in _categoryService.GetAll() select new CategoryWithCountModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ProductCount = _productService.GetProductCountByCategory(x) }).ToList();
            return View(product);
        }
    }
}