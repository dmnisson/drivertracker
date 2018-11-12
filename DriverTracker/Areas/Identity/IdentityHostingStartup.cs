using System;
using DriverTracker.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;

[assembly: HostingStartup(typeof(DriverTracker.Areas.Identity.IdentityHostingStartup))]
namespace DriverTracker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DriverTrackerIdentityDbContext>(options =>
                     options.UseSqlite("Data Source=DriverTracker.db"));

                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DriverTrackerIdentityDbContext>()
                .AddDefaultTokenProviders();

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                        .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Logout");
                });

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });
            });
        }


    }
}