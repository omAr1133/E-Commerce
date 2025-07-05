using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICashService,CashService>();

            services.AddScoped<Func<IProductService>>(provider=>()
            =>provider.GetRequiredService<IProductService>());
            services.AddScoped<Func<IAuthenticationService>>(provider => ()
            => provider.GetRequiredService<IAuthenticationService>());
            services.AddScoped<Func<IBasketService>>(provider => ()
            => provider.GetRequiredService<IBasketService>());
            services.AddScoped<Func<IOrderService>>(provider => ()
            => provider.GetRequiredService<IOrderService>());

            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            return services;
        }
    }
}
