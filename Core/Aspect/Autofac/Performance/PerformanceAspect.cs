using Castle.DynamicProxy;
using Core.IOC;
using Core.Utility.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspect.Autofac.Performance
{
    public class PerformanceAspect : MethodInterseption
    {
        public int _interval;
        private Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.Resolve<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName }.{invocation.Method.Name } --> {_stopwatch.Elapsed.TotalSeconds}");
            }

            _stopwatch.Reset();
        }
    }
}
