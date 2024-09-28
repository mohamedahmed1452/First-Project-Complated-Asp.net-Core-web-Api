using Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser
                {
                    DisplayName = "Mohamed",
                    Email = "Mohamed_Ahmed@gmail.com",
                    UserName = "MohamedAhmed",
                    PhoneNumber = "01119137448",
                };
                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
