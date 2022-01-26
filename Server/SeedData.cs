using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.Security.Claims;

namespace Server
{
    public static class SeedData
    {
        public static void EnsureSeedData(string connString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(options => 
                options.UseSqlServer(connString)
                );

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(Options =>
            {
                Options.ConfigureDbContext = db => db.UseSqlServer(connString, sql => 
                sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

            services.AddConfigurationDbContext(options => options.ConfigureDbContext = db => db.UseSqlServer(
                connString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)));

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();


            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);


        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach(var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach(var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();  
            }

            if (!context.ApiScopes.Any())
            {
                foreach(var scope in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach(var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }


        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var zhangsan = userMgr.FindByNameAsync("zhangsan").Result;
            if(zhangsan == null)
            {
                zhangsan = new IdentityUser
                {
                    UserName = "zhangsan",
                    Email = "zhangsan@mail.com",
                    EmailConfirmed = true,
                };

                var result = userMgr.CreateAsync(zhangsan, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(
                    zhangsan,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name,"Zhang San"),
                        new Claim(JwtClaimTypes.GivenName, "Zhang"),
                        new Claim(JwtClaimTypes.FamilyName,"San"),
                        new Claim(JwtClaimTypes.WebSite,"http://www.hipad.com"),
                        new Claim("location","somewhere")
                    }
                    ).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
