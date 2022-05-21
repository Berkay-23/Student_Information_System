using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using SISAPI.Persistence;

namespace SISAPI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceServices();
            services.AddControllersWithViews();
            services.AddSession();

            services.AddRazorPages()
               .AddMvcOptions(options =>
               {
                   options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Bu alan bo� b�rak�lamaz");
                   options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(_ => "Bu alan say� olmal�d�r. ");
                   options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "Ge�ersiz de�er girdiniz");
                   options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Girdi�iniz de�er bu alan i�in ge�erli de�il");
               });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env /*,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
