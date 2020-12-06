using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Entities.Identity;

namespace ThirdTryAPI.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mariam",
                    Email = "mariam@love.com",
                    UserName = "mariam@love.com",
                    Address = new Address
                    {
                        FirstName = "Mariam",
                        LastName = "Datunashvili",
                        Street = "10th Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "777"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
