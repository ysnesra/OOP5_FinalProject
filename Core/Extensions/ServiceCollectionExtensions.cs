using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Polimorfizm
        //AddDependencyResolvers extension metotuyla istediğimiz kadar ICoreModule'dan oluşturuduğumuz module ekleyebiliriz. Mesela; CoreModule bağımlılık modülü gibi birden fazla bağımlılığı yüklememizi sağlar
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);  //parametre olarak girilen herbir module'ü foreach ile tek tek yükle
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
