using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Service;

namespace Test_E_CommerceProject.Extensions
{
    public static class IdentityServicesExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));//
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication(options =>
            {
                // Set the default authentication and challenge schemes to JWT
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //configure authentication Handler
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ClockSkew =TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"])),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                    };
                });

            return services;


        }
    }
}
