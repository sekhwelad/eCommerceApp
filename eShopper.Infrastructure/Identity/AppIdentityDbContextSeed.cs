using eShopper.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace eShopper.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Delight",
                    Email = "delight@test.com",
                    UserName = "delight@test.com",
                    Address = new Address
                    {
                        FirstName = "Delight",
                        LastName = "Sekhwela",
                        Street = "10 The Street",
                        City = "JHB",
                        State = "SA",
                        ZipCode = "0000"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }
    }
}
