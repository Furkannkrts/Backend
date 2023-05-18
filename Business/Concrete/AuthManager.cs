using Business.Abstract;
using Business.Constant;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;//burayı kullanıcaz çünkü kullanıcı veritabanında var mı buradan bakılır.
        private ITokenHelper _tokenHelper;//kullanıcı login olduğunda token vermek için  

        
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken=_tokenHelper.CreateToken(user, claims);
            return new SuccesDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            //önce maili kontrol ediyor sonra password'u
            var userToCheck=_userService.GetByMail(userForLoginDto.Email);
            if(userToCheck == null)//kullanıcı var mı kontrolü
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if(!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
                    
            }
            return new SuccesDataResult<User>(userToCheck,Messages.SuccessfulLogin);
        }
        //register işleminin gerçekleştirilmesi
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FİrstName=userForRegisterDto.FirstName,
                LastName=userForRegisterDto.LastName,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                Status=true

            };
            _userService.Add(user);
            return new SuccesDataResult<User>(user, Messages.UserRegistered);
        }

        //kullanıcı kontrolü(login işleminin tamamlanması
        public IResult UserExists(string email)
        {
            if(_userService.GetByMail(email)!=null)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
