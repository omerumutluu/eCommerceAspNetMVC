using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetByCategoryId(int categoryId)
        {
            return _categoryDal.Get(category => category.CategoryId == categoryId);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category.CategoryId, category);
        }
    }
}
