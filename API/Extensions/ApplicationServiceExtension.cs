using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenricRepository<>),(typeof(GenricRepository<>)));
             services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory =ActionContext =>
                {
                    var errors= ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage) .ToArray();

                    var errorResponse = new ValidationErrorResponse
                    {
                       Errors= errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;

        }
    }
}