﻿using eShopper.Core.Interfaces;
using eShopper.Errors;
using eShopper.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopper.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                      policy =>
                                      {
                                          policy.WithOrigins("https://localhost:4200")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                                      });
            });


            return services;
        }
    }
}
