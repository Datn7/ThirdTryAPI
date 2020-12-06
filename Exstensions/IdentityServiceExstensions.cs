using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdTryAPI.Data;
using ThirdTryAPI.Entities.Identity;

namespace ThirdTryAPI.Exstensions
{
    public static class IdentityServiceExstensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration Config)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Token:Key"])),
                    ValidIssuer = Config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience=false
                };
            });

            return services;
        }
    }
}
