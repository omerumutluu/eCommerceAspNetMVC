using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Repository.EntityFramework
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class
    {
        DataAccess.Context.Context context = new DataAccess.Context.Context();

        public void Add(TEntity entity)
        {

            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();

            //context.Set<TEntity>().Add(entity);
            //context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();

            //context.Set<TEntity>().Remove(entity);
            //context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Detached;
            context.SaveChanges();
        }

        public void Update(int id, TEntity entity)
        {
            var existingEntity = context.Set<TEntity>().Find(id);
            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }
    }
}
