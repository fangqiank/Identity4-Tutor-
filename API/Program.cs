using API.Service;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<ICoffeeShopService, CoffeeShopService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
