using System.Web;
using System.Web.Mvc;

namespace DriverTracker.Controllers {
   public class AnalystController : Controller {
      private AnalystDBContext db = new AnalystDBContext();

      //
      // GET: /Analyst/

      public string Dashboard() {
          return View(db.Analyst.Find(HttpContext.Current.User));
      }
   }
}
