using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        void Add(Product product);
        void Update(Product product);
        void Update(int id, Product entity);
        void Delete(Product product);
        List<Product> GetAll();
        List<Product> GetAllIsHaveStock();
        Product GetByProductId(int productId);
        List<Product> GetByCategoryId(int categoryId);
        List<Product> GetByCorporateCustomerId(int corporateCustomerId);
        List<Product> GetAllByNameSearch(string nameSearch);
        List<Product> GetAllByPrice(int minimumValue, int maximumValue);
        List<Product> GetAllByPriceAsc();
        List<Product> GetAllByPriceDesc();
        int GetProductCountByCategory(Category category);
    }
}
