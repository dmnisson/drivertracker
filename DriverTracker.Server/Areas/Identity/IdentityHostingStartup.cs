using DriverTracker.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;

[assembly: HostingStartup(typeof(DriverTracker.Areas.Identity.IdentityHostingStartup))]
namespace DriverTracker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddCors(options =>
                {
                    options.AddPolicy("AuthCors", corsBuilder =>
                    {
                        corsBuilder.AllowAnyOrigin();
                        corsBuilder.AllowAnyMethod();
                        corsBuilder.AllowAnyHeader();

                    });
                });

                services.AddDbContext<DriverTrackerIdentityDbContext>(options =>
                     options.UseSqlite("Data Source=DriverTracker.db"));

                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DriverTrackerIdentityDbContext>();

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.Configuration["APITokens:Key"]));
                services.AddAuthentication().AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false; // this line in development version only
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = context.Configuration["APITokens:Audience"],
                        ValidIssuer = context.Configuration["APITokens:Issuer"]
                    };
                });

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