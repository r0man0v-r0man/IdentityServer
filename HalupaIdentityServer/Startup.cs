using HalupaIdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HalupaIdentityServer
{
    public class Startup

    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlite(connectionString);
                //config.UseInMemoryDatabase("Memory");
            });

            // AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Login");
                config.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Logout");
            });
            services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(Configuration.GetApis())
                .AddInMemoryClients(Configuration.GetClients())
                .AddInMemoryApiScopes(Configuration.GetScopes())
                .AddDeveloperSigningCredential();
            services
                .AddCors();
            services
                .AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();

            app.UseIdentityServer();
            if (env.IsDevelopment())
            {
                app.UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
