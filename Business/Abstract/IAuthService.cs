using Core.Entities.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRigsterDto rigsterDto, string password);
        IDataResult<User> Login(UserForLoginDto loginDto);
        IResult UserExists(string email);
        IDataResult<AccsessToken> CreateAccsessToken(User user);
    }
}
