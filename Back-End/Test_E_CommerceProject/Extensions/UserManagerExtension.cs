using Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Test_E_CommerceProject.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(A => A.Address).SingleOrDefaultAsync(U => U.Email == Email);
       
           return user;
        }
    }
}
