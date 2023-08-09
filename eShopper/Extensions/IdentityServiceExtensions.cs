using eShopper.Core.Entities.Identity;
using eShopper.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShopper.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<AppUser>(options =>
            {

            })
             .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            services.AddAuthorization();
            return services;
        }
    }
}
