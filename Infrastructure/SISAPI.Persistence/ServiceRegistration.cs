using Microsoft.Extensions.DependencyInjection;
using SISAPI.Application.Repositories;
using SISAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using SISAPI.Persistence.Contexts;
using SIS.Persistence;
using SISAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;

namespace SISAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<SISContext>(options =>
            options.UseSqlServer(Configuration.ConnectionString), ServiceLifetime.Scoped);


            services.AddIdentity<AppUser, IdentityRole>(
                opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<SISContext>();

            services.ConfigureApplicationCookie(option =>
            {
                option.AccessDeniedPath = new PathString("/Home/Index");
                option.LoginPath = new PathString("/Home/Index");
                option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                option.Cookie.SameSite = SameSiteMode.Strict;
                option.Cookie.Name = "LoginCookie";
                option.Cookie.HttpOnly = true;
            });

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IAcademicRepository, AcademicRepository>();
        }
    }
}