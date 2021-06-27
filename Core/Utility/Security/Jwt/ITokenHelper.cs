using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Security.Jwt
{
    public interface ITokenHelper
    {
        AccsessToken CreateToken(User user,List<OperationClaim> operationClaims);
    }
}
