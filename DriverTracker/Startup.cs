﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;
using DriverTracker.Data;
using DriverTracker.Domain;
using DriverTracker.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Accord.Statistics.Analysis;

namespace DriverTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);



            services.AddDbContext<MvcDriverContext>(options => options.UseSqlite("Data Source=DriverTracker.db"));
            services.AddDbContext<DriverTrackerIdentityDbContext>(options =>
                                                                  options.UseSqlite("Data Source=DriverTracker.db"));

            services.AddAuthorization(options => {
                options.AddPolicy("DriverInfoPolicy", policy => policy.Requirements.Add(new SameDriverRequirement()));
            });
            services.AddAuthorization(options => {
                options.AddPolicy("DriverInfoPolicyPickupAllowed", policy => policy.Requirements.Add(new SameDriverRequirement(true)));
            });

            services.AddScoped<IAuthorizationHandler, UserInfoPermissionHandler>();

            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ILegRepository, LegRepository>();
            services.AddScoped<IPickupRequestRepository, PickupRequestRepository>();
            services.AddScoped<IGeocodingDbSync>(provider => new GeocodingDbSync(
                this.Configuration, provider.GetService<MvcDriverContext>()));
            services.AddScoped<ILocationClustering, LocationClustering>();
            services.AddScoped<IPickupPrediction>(provider => new PickupPrediction(
                provider.GetService<ILocationClustering>(),
                provider.GetService<ILegRepository>(),
                provider.GetService<IGeocodingDbSync>(),
                new LogisticRegressionAnalysis
                {
                    Inputs = new string[] { "pickupDelay", "duration" },
                    Output = "inFareClass"
                }
                ));
            services.AddSingleton(this.Configuration);
                                                                 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
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
            app.UseCors("AuthCors");
            app.UseAuthentication();
            app.UseCookiePolicy();

            _ = app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      "default",
                      "{controller=Home}/{action=Index}/{id?}");
              });

            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // required services for adding our roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] dtRoleNames = { "Admin", "Analyst", "Driver", "PickupRequestSystem" };
            IdentityResult roleResult;

            foreach (var roleName in dtRoleNames)
            {
                // create and seed roles
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }


            }

            // TODO create a middleware class that allows first-time user to configure admin username and password
            var adminuser = new IdentityUser
            {
                UserName = Configuration.GetSection("AdminUser")["UserName"],
                Email = Configuration.GetSection("AdminUser")["Email"]
            };

            string AdminPassword = Configuration.GetSection("AdminUser")["Password"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("AdminUser")["Email"]);

            if (_user == null) {
                var _createAdminResult = await UserManager.CreateAsync(adminuser, AdminPassword);
                if (_createAdminResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(adminuser, "Admin");
                }
            }
        }
    }


}
