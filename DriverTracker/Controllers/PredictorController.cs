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
        [Authorize]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}