using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utility.Interceptors;
using Core.Utility.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspect.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterseption
    {
        private LoggerServiceBase _loggerServiceBase;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(CoreMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnExeption(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWith = GetLogDetail(invocation);
            logDetailWith.ExceptionMessage = e.Message;
            _loggerServiceBase.Error(logDetailWith);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetial = new LogDetailWithException
            {
                Target = invocation.InvocationTarget?.ToString(),
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetial;
        }
    }
}
