using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DriverTracker.ViewComponents
{
    public class CookieConsentViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;

        public CookieConsentViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["CookiePolicyMessage"] = _configuration.GetSection("CompanyInfo")["CookiePolicyMessage"];

            return View();
        }
    }
}
