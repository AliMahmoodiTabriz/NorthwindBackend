using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.IOC;
using Core.Utility.Exceptions;
using Core.Utility.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterseption
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.Resolve<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthException(Messages.AuthenticationDenied,Messages.AuthenticationDeniedId);

            var roleClaim = _httpContextAccessor.HttpContext.User.ClaimRoles();

            foreach (var role in roleClaim)
            {
                if (_roles.Contains(role))
                    return;
            }

            throw new AuthException(Messages.AuthorizeationDenied,Messages.AuthorizeationDeniedId);
        }
    }
}
