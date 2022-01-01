using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IGenericDal<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter=null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
    }
}
