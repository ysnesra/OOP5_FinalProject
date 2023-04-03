using Core.CrossCuttingConcerns.Cashing;
using Core.CrossCuttingConcerns.Cashing.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //Startup daki Innjection kodumuzu buraya taşırız(.Net'in kendi IoC yapısını kullanarak yazıyoruz )
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddMemoryCache(); //IMemoryCache'in arka planda instance nı oluşturur (microsofttan gelecek)
            serviceCollection.AddSingleton<Stopwatch>();  //zamanlayıcı,kronometre , PerformanceAspect classında kullanıyoruz
        }
    }
}
