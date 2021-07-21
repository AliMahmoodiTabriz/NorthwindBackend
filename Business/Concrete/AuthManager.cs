using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using Core.Utility.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRigsterDto rigsterDto,string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email= rigsterDto.Email,
                FirstName= rigsterDto.FirstName,
                LastName= rigsterDto.LostName,
                PasswordHash= passwordHash,
                PasswordSalt= passwordSalt,
                Status=true,
            };
            _userService.Add(user);

            return new SuccsessDataResult<User>(user,Messages.UserRegistered, Messages.UserRegisteredId);
        }

        public IDataResult<User> Login(UserForLoginDto loginDto)
        {
            var userCheck = _userService.GetByMail(loginDto.Email);

            if(userCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound, Messages.UserNotFoundId);
            }

            if(!HashingHelper.VerifyPasswordHash(loginDto.Password, userCheck.PasswordHash, userCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError, Messages.PasswordErrorId);
            }

            return new SuccsessDataResult<User>(userCheck,Messages.SuccessfulLogin, Messages.SuccessfulLoginId);
        }

        public IResult UserExists(string email)
        {
            if(_userService.GetByMail(email)!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExists, Messages.UserAlreadyExistsId);
            }

            return new SuccsessResult();
        }

        public IDataResult<AccsessToken> CreateAccsessToken(User user)
        {
            var cliam = _userService.GetClaims(user);
            var acssessToken = _tokenHelper.CreateToken(user,cliam);
            return new SuccsessDataResult<AccsessToken>(acssessToken);
        }
    }
}
