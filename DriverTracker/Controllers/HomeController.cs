using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DriverTracker.Models;

namespace DriverTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "DriverTracker is a system for analyzing the performance of drivers for taxicabs and transportation network carriers.";

            return View();
        }

        public IActionResult Contact()
        {
            IConfigurationSection companyInfoSection = _configuration.GetSection("CompanyInfo");
            ViewData["Message"] = companyInfoSection["ContactInfo"];
            ViewData["AddressLine1"] = companyInfoSection["AddressLine1"];
            ViewData["AddressLine2"] = companyInfoSection["AddressLine2"];
            ViewData["AddressLine3"] = companyInfoSection["AddressLine3"];
            ViewData["Telephone"] = companyInfoSection["Telephone"];
            ViewData["Email"] = companyInfoSection["Email"];

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["PrivacyPolicy"] = _configuration.GetSection("CompanyInfo")["PrivacyPolicy"];


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
