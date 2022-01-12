using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;

var connString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AspNetIdentityDbContext>(options => 
    options.UseSqlServer(
        connString, x => x.MigrationsAssembly(assembly)
        ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
{
    options.ConfigureDbContext = x => x.UseSqlServer(connString, opt => opt.MigrationsAssembly(assembly));
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = x => x.UseSqlServer(connString, opt => opt.MigrationsAssembly(assembly));
})
.AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
