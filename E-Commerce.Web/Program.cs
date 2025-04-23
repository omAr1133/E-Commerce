
using System.Threading.Tasks;
using Domain.Contracts;
using E_Commerce.Web.MiddleWares;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstractions;

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
            builder.Services.AddSwaggerGen();

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
