using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //1) header
            //2) payload => private claims (User-Defined)
            var authClaim = new List<Claim>() {
            new Claim(ClaimTypes.GivenName, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),

            };
            var userRoles = userManager.GetRolesAsync(user).Result;
            foreach(var role in userRoles)
                authClaim.Add(new Claim(ClaimTypes.Role, role));


            //3) signature
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
              issuer: _configuration["JWT:ValidIssuer"],
              audience: _configuration["JWT:ValidAudience"],
              expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
              claims: authClaim,
              signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
              );
            var tokenHandler = new JwtSecurityTokenHandler();
            return Task.FromResult(tokenHandler.WriteToken(token));

        }


    }
}
