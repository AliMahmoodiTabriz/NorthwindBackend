using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utility.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utility.Security.Jwt
{
    public class TokenHelper : ITokenHelper
    {
        private IConfiguration _configuration;
        private DateTime _accsessTokenExpiration;
        private TokenOption _tokenOption;
        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOption = _configuration.GetSection("TokenOption").Get<TokenOption>();
            
        }
        public AccsessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accsessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var key = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(key);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, operationClaims);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.WriteToken(jwt);

            return new AccsessToken
            {
                Token = token,
                Expiration = _accsessTokenExpiration,
            };
        }
        public JwtSecurityToken CreateJwtSecurityToken(User user,SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer : _tokenOption.Issuer,
                audience: _tokenOption.Audience,
                expires: _accsessTokenExpiration,
                notBefore: DateTime.Now,
                claims : setClaim(user,operationClaims),
                signingCredentials:signingCredentials
                ) ;
            return jwt;
        }
        private IEnumerable<Claim> setClaim(User user,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName}.{user.LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());

            return claims;
        }
    }
}
