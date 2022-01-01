using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Repository.EntityFramework
{
    public class EfCategoryDal : EfGenericRepository<Category>, ICategoryDal
    {
    }
}