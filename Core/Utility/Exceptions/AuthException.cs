using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Core.Utility.Exceptions
{
    public class AuthException: AuthenticationException
    {
        public AuthException(string message):base(message)
        {

        }
    }
}
