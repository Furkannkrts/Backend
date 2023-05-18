using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class,IEntity, new()
    where TContext : DbContext, new()
    {
        public void add(TEntity entity)
        {
            using (var context = new TContext())//using=> disposable pattern.
                                                //Pahalı bir nesne olduğu için TContext
                                                //using verince garbage collector'a bırakmıyor
            {
                var addedEntity=context.Entry(entity);
                addedEntity.State= EntityState.Added;
                context.SaveChanges();
            }


        }

        public void delete(TEntity entity)
        {

            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                //if(filter == null)
                //{
                //    var a =context.Set<TEntity>().ToList();
                //    return a;
                //}////////////////////////////////////////////////////////// alttakinin  aynısının hata yönetimini  bu şekilde yapabiliriz.
                //else//debugger
                //{
                //    var c= context.Set<TEntity>().Where(filter).ToList();
                //    return c;
                //}
                return filter==null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void update(TEntity entity)
        {

            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }

}
