using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Errors;
using ThirdTryAPI.Interfaces;
using ThirdTryAPI.Repositories;

namespace ThirdTryAPI.Exstensions
{
    public static class ApplicationServicesExstensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            //SQL bazastan cvdoma productRepository-t
            services.AddScoped<IProductRepository, ProductRepository>();

            //SQL bazastan Generic repository-t cvdoma
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Basket repository gamoyeneba
            services.AddScoped<IBasketRepository, BasketRepository>();

            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
