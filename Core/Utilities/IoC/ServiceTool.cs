using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    //Extensions Method
    public static class ServiceTool
    {
        //ServiceTool classı ile herhangi bir Interfacein karşılığını (somut classını) alabiliriz
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            //.Net in servislerini (IServiceCollection) al -> onları kendine Build et
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}