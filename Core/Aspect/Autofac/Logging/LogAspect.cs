using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.IOC;
using Core.Utility.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspect.Autofac.Logging
{
    public class LogAspect: MethodInterseption
    {
        private LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception("Wrong Logger Type");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation)) ;
        }

        private object GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name= invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value=invocation.Arguments[i],
                    Type= invocation.Arguments[i].GetType().Name
                });
            }

            var logDetial = new LogDetail
            {
                Target= invocation.InvocationTarget?.ToString(),
                MethodName= invocation.Method.Name,
                LogParameters= logParameters
            };

            return logDetial;
        }
    }
}
