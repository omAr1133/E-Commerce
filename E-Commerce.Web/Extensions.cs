﻿using Domain.Contracts;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {  
           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Func<ActionContext, IActionResult>
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIValidationResponse;
            });
            services.AddSwaggerServices();
            return services;
        }

        public static async Task<WebApplication> InitializeDataBaseAsync (this WebApplication app)
        {  
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;
    }

    }
}
