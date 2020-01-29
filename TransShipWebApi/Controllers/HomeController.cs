using System.Web.Mvc;

namespace TransShipWebApi.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Mauro Ortega";

            return View();
        }
    }
}
