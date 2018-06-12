using IdentityDeepDive.Data;
using IdentityDeepDive.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace IdentityDeepDive
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
            services.AddMvc();
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;database=PluralsightIdentityDemo.PluralsightUser;trusted_connection=yes;";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<PluralsightUserDbContext>(opt => opt.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<PluralsightUser, IdentityRole>(options => 
                {
                    //options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = "emailconf";
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 4;
                    options.User.RequireUniqueEmail = true;

                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 3; //default is 5
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); //default is 5


                })
                .AddEntityFrameworkStores<PluralsightUserDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<PluralsightUser>>("emailconf")
                .AddPasswordValidator<DoesNotContainPasswordValidator<PluralsightUser>>();

            services.AddScoped<IUserClaimsPrincipalFactory<PluralsightUser>,
                PluralsightUserClaimsPrincipalFactory>();

            //Forgot Password/Reset Password token options
            services.Configure<DataProtectionTokenProviderOptions>(opt => 
                opt.TokenLifespan = TimeSpan.FromHours(3));

            //Email Confirmation token options
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(2));

            services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Home/Login");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
