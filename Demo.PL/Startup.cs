using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }//Configration=>Catch File AppSeeting ,get=>(Read Only)

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MVCAppDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });//Allow Dependancy Injection

            services.AddScoped<IDepartmetRepository,DepartmentRepository>();//Allow Dependancy Injection For Class DepartmentRepository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();//Allow Dependancy Injection For Class EmployeeRepository


            #region First Choice To Make Mapper TO 2 Mappers(EmployeeProfile,UserProfile)
            //services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()));
            //services.AddAutoMapper(M => M.AddProfile(new UserProfile())); 
            #endregion


            # region Second Choice To Make Mapper TO 2 Mappers(EmployeeProfile,UserProfile) using Obbject
            services.AddAutoMapper(M => M.AddProfiles(new List<Profile>() { new EmployeeProfile(), new UserProfile(),new RoleProfile() })); 
            #endregion


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                    Options.Password.RequireNonAlphanumeric = true;// @ $ #
                    Options.Password.RequireDigit = true;//1234
                    Options.Password.RequireLowercase = true;//ajsnj
                    Options.Password.RequireUppercase = true;//ASSDD
                   //P@ssw0rd
                   //Pa$$w0rd 

            })//used to add interfaces
                     .AddEntityFrameworkStores<MVCAppDbContext>()//used to add Classes
                     .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options =>
            {
                Options.LoginPath = "Account/Login";
                Options.AccessDeniedPath = "Home/Error";
            });//Used to add services =>(1)User Manager
                                                                //(2)Sign In Manager
                                                                //(3)Role Manager
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
                    name: "default",
					//pattern: "{controller=Home}/{action=Index}/{id?}");
					pattern: "{controller=Account}/{action=Login}/{id?}");
			});
        }
    }
}
