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
                   options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Bu alan boþ býrakýlamaz");
                   options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(_ => "Bu alan sayý olmalýdýr. ");
                   options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "Geçersiz deðer girdiniz");
                   options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Girdiðiniz deðer bu alan için geçerli deðil");
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
