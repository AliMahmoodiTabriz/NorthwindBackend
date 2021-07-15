using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IOC
{
    public static class ServiceTool
    {
        private static IServiceProvider _serviceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection service)
        {
            _serviceProvider = service.BuildServiceProvider();
            return service;
        }
        public static T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
