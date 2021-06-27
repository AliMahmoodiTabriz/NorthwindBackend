using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Security.Jwt
{
    public class AccsessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
