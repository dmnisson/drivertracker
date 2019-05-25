using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DriverTracker.Controllers
{
    public class PredictorController : Controller
    {
        [Authorize(Roles = "Admin,Analyst")]
        public IActionResult Index()
        {
            return View();
        }
    }
}