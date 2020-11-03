using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;
using ExpenseTracker.Business.Services.Email;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.DataProtection;

namespace ExpenseTracker.WebUI
{
    public class Startup
    {
        public IWebHostEnvironment Env { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                    .SetApplicationName($"expense-tracker-{Env.EnvironmentName}")
                    .PersistKeysToFileSystem(new DirectoryInfo($@"{Env.ContentRootPath}\keys"))
                    .SetDefaultKeyLifetime(System.TimeSpan.FromDays(1000));
            //.DisableAutomaticKeyGeneration(); -> https://stackoverflow.com/a/43327546/837560

            services.AddDbContext<ExpenseTrackerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ExpenseTrackerDbContext>();

            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSender"));

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddControllersWithViews();

            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    //new CultureInfo("tr-TR"),
                    new CultureInfo("en-US")
                };
                //options.DefaultRequestCulture = new RequestCulture("tr-TR");
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
