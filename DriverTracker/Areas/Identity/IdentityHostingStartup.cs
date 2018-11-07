using System;
using DriverTracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<DriverTrackerIdentityDbContext>();
            });
        }
    }
}