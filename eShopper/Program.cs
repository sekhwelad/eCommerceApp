using eShopper.Core.Entities.Identity;
using eShopper.Extensions;
using eShopper.Infrastructure.Data;
using eShopper.Infrastructure.Identity;
using eShopper.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();    



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwaggerDocumentation();

app.UseStaticFiles();
app.UseAuthentication();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<StoreContext>();
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();

var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
   // await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    //await AppIdentityDbContextSeed.SeedUserAsync(userManager);
}
catch (Exception ex)
{

    logger.LogError(ex, "An error occured during migration");
}


app.Run();