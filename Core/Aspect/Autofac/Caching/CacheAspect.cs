using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.IOC;
using Core.Utility.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Aspect.Autofac.Caching
{
    public class CacheAspect: MethodInterseption
    {
        private int _duration;
        private ICacheManager _cacheManager;
        public CacheAspect(int duration)
        {
            _duration = duration;
            _cacheManager = ServiceTool.Resolve<ICacheManager>();
        }
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = GetFieldsOfClass(invocation.Arguments);
            var key = $"{methodName}({arguments})";
            if(_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }

        private string GetFieldsOfClass(params object[] entity) 
        {
            List<string> result = new List<string>();
            foreach (var item in entity)
            {
                result.Add(string.Join(",", item.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x?.GetValue(item) != null)
                    .Select(x => x?.GetValue(item)).ToList()));               
            }

            return string.Join(",", result);
        }
    }
}
