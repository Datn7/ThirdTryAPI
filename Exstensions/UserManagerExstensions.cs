using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ThirdTryAPI.Entities.Identity;

namespace ThirdTryAPI.Exstensions
{
    public static class UserManagerExstensions
    {
        public static async Task<AppUser> FindUserByClaimsPricipleWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user) 
        {
            var email = user.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPriciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
