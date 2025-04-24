
using System.Threading.Tasks;
using Domain.Contracts;
using E_Commerce.Web.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstractions;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IDbInitializer,DbInitializer>();
            builder.Services.AddScoped<IServiceManager, ServiceManager> ();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                // Func<ActionContext, IActionResult>
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    // Get the Entries in Model Stat that has Validation errors
                    var errors = context.ModelState.Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(error => error.ErrorMessage)
                        });
                    var response = new ValidationErrorResponse { ValidationErrors = errors };
                    return new BadRequestObjectResult(response);
                };
            });


            var app = builder.Build();
            await InitializeDbAsync(app);
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("processing Request");
            //    await next.Invoke();
            //    Console.WriteLine("Writing Response");
            //    Console.WriteLine(context.Response);
            //});
               
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
        }
    }
}
