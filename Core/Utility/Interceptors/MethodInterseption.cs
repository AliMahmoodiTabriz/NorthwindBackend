using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Interceptors
{
    public class MethodInterseption: MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnExeption(IInvocation invocation, Exception e) { }
        protected virtual void OnSuccsess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation) 
        {
            var isSuccsess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccsess = false;
                OnExeption(invocation,e);
                throw;
            }
            finally
            {
                if(isSuccsess)
                {
                    OnSuccsess(invocation);
                }
            }

            OnAfter(invocation);
        }
    }
}
