using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //ınsert,update,dalete operasyonlarımızın gerçekleştiği yer/REPOSİTORY PATTERN
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        T get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter=null) ;
        void add (T entity);
        void update (T entity);
        void delete(T entity);
    }
}
