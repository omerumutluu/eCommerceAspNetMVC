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
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByNameSearch(string nameSearch)
        {
            return _productDal.GetAll(product => product.ProductName.ToLower().Contains(nameSearch) && product.UnitsInStock > 0);
        }

        public List<Product> GetAllByPrice(int minimumValue, int maximumValue)
        {
            return _productDal.GetAll(product => product.UnitPrice >= minimumValue && product.UnitPrice <= maximumValue && product.UnitsInStock > 0);
        }

        public List<Product> GetAllByPriceAsc()
        {
            return _productDal.GetAll(product => product.UnitsInStock > 0).OrderBy(product => product.UnitPrice).ToList();
        }

        public List<Product> GetAllByPriceDesc()
        {
            return _productDal.GetAll(product => product.UnitsInStock > 0).OrderByDescending(product => product.UnitPrice).ToList();
        }

        public List<Product> GetAllIsHaveStock()
        {
            return _productDal.GetAll(product => product.UnitsInStock > 0);
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _productDal.GetAll(product => product.CategoryId == categoryId && product.UnitsInStock > 0);
        }

        public List<Product> GetByCorporateCustomerId(int corporateCustomerId)
        {
            return _productDal.GetAll(product => product.CorporateAccountId == corporateCustomerId);
        }

        public Product GetByProductId(int productId)
        {
            return _productDal.Get(product => product.ProductId == productId);
        }

        public int GetProductCountByCategory(Category category)
        {
            return _productDal.GetAll(product => product.CategoryId == category.CategoryId).Count;
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Update(int id, Product product)
        {
            _productDal.Update(id, product);
        }
    }
}
