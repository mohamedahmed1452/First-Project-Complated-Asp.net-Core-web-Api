
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Test_E_CommerceProject.Service.Errors;
using Test_E_CommerceProject.Service.Extensions;
using Test_E_CommerceProject.Service.Helpers;
using Test_E_CommerceProject.Service.MiddleWares;

using Talabat.Core;
using Talabat.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Service;
using Test_E_CommerceProject.Extensions;



namespace Test_E_CommerceProject
{
    public class Program
    {
         
        public static async Task Main(string[] args)
          {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddSwaggerServices();
            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });
            webApplicationBuilder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });
            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider =>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            }));
            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    option =>
                    {
                        option.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            }); 
            #endregion


            //MiddleWare 
            var app = webApplicationBuilder.Build();



            #region Update DataBase
           
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext=services.GetRequiredService<AppIdentityDbContext>();
            //Logger Factory ->Log Error In Kestrel 
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();//created in AddController
            
            try
            {
                await _dbContext.Database.MigrateAsync();//update database Done
                await StoreContextSeeding.SeedingAsync(_dbContext);//Seed Data Done
                await _identityDbContext.Database.MigrateAsync();//update database Done
                var _userManager = services.GetRequiredService<UserManager<AppUser>>();//Explicitly
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManager);//Seed Data Done
            }
            catch (Exception ex)
            {
                var Logger = loggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }


            #endregion

            #region Configure Kestrel Middleware 


            app.UseMiddleware<ExceptionMeddleWare>();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization(); 
            #endregion


            app.Run();
        }
    }
}
