using System.Web;
using Microsoft.AspNetCore.Mvc;

using DriverTracker.Models;

namespace DriverTracker.Controllers
{
    public class AnalystController : Controller
    {
        private AnalystDBContext db = new AnalystDBContext();

        //
        // GET: /Analyst/

        public string Dashboard()
        {
            return View(db.Analysts.Find(HttpContext.User));
        }
    }
}
