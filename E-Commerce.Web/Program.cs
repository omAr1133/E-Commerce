
using System.Threading.Tasks;
using Domain.Contracts;
using E_Commerce.Web.Factories;
using E_Commerce.Web.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstractions;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options=>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
           
            builder.Services.AddApplicationServices(builder.Configuration);
            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);

            var app = builder.Build();

            //await InitializeDbAsync(app);
            await app.InitializeDataBaseAsync();
            //app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseCustomExceptionMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options=>
                {
                    options.DocumentTitle = "My E-commeerce API";
                    options.DocExpansion(DocExpansion.None);
                    options.EnableFilter();
                    options.DisplayRequestDuration();
                });
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

       
    }
}
