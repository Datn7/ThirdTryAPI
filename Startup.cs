using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Data;
using ThirdTryAPI.Errors;
using ThirdTryAPI.Exstensions;
using ThirdTryAPI.Helpers;
using ThirdTryAPI.Interfaces;
using ThirdTryAPI.Middleware;
using ThirdTryAPI.Repositories;

namespace ThirdTryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //SQL bazastan cvdoba contextistvis
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //Use AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles));

            //use expanded external ApplicationServicesExstensions to add services
            services.AddApplicationServices();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            //use created exstension to add swagger
            services.AddSwaggerDocumentation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            /*
              if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             */

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            //statikuri failebi
            app.UseStaticFiles();

            //add CORS
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            //use swagger documentation exstension
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
