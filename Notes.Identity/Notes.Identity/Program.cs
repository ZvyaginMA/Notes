using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Notes.Identity;
using Notes.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration["DbConnection"];
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(configure =>
{
    configure.Password.RequiredLength = 4;
    configure.Password.RequireDigit = false;
    configure.Password.RequireNonAlphanumeric = false;
    configure.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Notes.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/logout";
});

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    try
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(dbContext);
    }
    catch (Exception ex)
    {
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while app initialization");
    }
}


app.UseRouting();
app.UseIdentityServer();
app.MapGet("/", () => "Hello World!");
app.Run();
