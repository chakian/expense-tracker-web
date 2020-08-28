using EFEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string _connectionString = null;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<ExpenseTrackerContext>(options =>
            {
                options.UseSqlServer(_connectionString,
                    d => d.MigrationsAssembly("EFEntities"));
            });

            ConfigureAuthorization(services);
            ConfigureIdentity(services);

            services.AddControllersWithViews();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            // TODO: This block remains here for future reference
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "ApiUser",
                    policy => policy.RequireClaim("role", "budget_owner"));
            });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            // add identity
            var identityBuilder = services.AddIdentityCore<Users>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<ExpenseTrackerContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "login",
                    defaults: new { controller = "Home", action = "Login" }
                    );
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
