using AP204_Pronia.DAL;
using AP204_Pronia.Hubs;
using AP204_Pronia.Models;
using AP204_Pronia.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<LayoutService>();
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>(opsion =>
               {
                   opsion.Password.RequireDigit = true;
                   opsion.Password.RequireLowercase = true;
                   opsion.Password.RequiredLength = 8;
                   opsion.Password.RequireNonAlphanumeric = false;
                   opsion.Password.RequireUppercase = false;

                   opsion.Lockout.MaxFailedAccessAttempts = 3;
                   opsion.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                   opsion.Lockout.AllowedForNewUsers = true;

                   opsion.SignIn.RequireConfirmedEmail = false;

                   opsion.User.AllowedUserNameCharacters = "qwertyuioplkjhgfddsazxcvbnm_";
               }
              
            ).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            services.AddHttpContextAccessor();
            services.AddSignalR();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                  );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{Id?}"
                    );
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
