using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Core.Aspect.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Aspect.Autofac.Exception;

namespace Core.Utility.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(JsonFileLogger)));
            classAttributes.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));
            classAttributes.Add(new LogAspect(typeof(DatabaseLogger)));
            return classAttributes.OrderByDescending(x => x.Priority).ToArray();
        }
    }
}