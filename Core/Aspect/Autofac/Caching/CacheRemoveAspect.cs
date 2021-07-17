using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.IOC;
using Core.Utility.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspect.Autofac.Caching
{
    public class CacheRemoveAspect: MethodInterseption
    {
        private string _pattern;
        private ICacheManager _cache;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cache = ServiceTool.Resolve<ICacheManager>();
        }

        protected override void OnSuccsess(IInvocation invocation)
        {
            _cache.RemoveByPattern(_pattern);
        }
    }
}
