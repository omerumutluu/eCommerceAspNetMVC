using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);
        Brand GetByBrandId(int brandId);
        List<Brand> GetAll();
    }
}
