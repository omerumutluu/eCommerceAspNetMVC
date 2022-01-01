using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce_Asp.Net_MVC.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService = new CategoryManager(new EfCategoryDal());

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}