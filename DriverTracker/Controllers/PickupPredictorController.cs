using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverTracker.Controllers
{
    public class PickupPredictorController : Controller
    {
        // GET: /<controller>/
        [Authorize(Roles = "Admin,Analyst")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
