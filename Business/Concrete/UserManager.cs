using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Concrete
{
   
    public class UserManager : IUserService
    {

        IUserDal _userDal;//dependency injection vasıtasıyla conctructor et

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.get(u=>u.Email== email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);//add ile farkını sor
        }

        
    }
}
