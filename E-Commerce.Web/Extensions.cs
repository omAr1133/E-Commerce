using System.Text;
using Domain.Contracts;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication;

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

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Func<ActionContext, IActionResult>
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIValidationResponse;
            });
            services.AddSwaggerServices();
            ConfigureJWT(services, configuration); 
            return services;
        }

        private static void ConfigureJWT(IServiceCollection services , IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer=jwt.Issuer,

                    ValidateAudience = true,
                    ValidAudience=jwt.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                };
            });
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
