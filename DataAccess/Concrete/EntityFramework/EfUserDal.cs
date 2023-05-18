using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)//list gönderdiğim için list döndüm.
           

        {
            using (var context = new NorthwindContext()) //user'ın özelliklerini join operasyonuyla çektik
            {
                var result = from operationClaim in context.OperationClaims
                             join useroperationClaim in context.UserOperationClaims
                                on operationClaim.Id equals useroperationClaim.Id
                             where useroperationClaim.UserId == user.Id
                             select new OperationClaim { Id = useroperationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

                             
            }
        }
    }
}
