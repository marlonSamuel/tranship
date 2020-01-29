using System.Web.Mvc;
using TransShip.Models;
using TransShipModel.Abstract;
using TransShip.LogBook;
using System.Web.Security;
using TransShipModel.DTO;
using System.Collections.Generic;

namespace TransShip.Controllers
{
    public class CustomerController : Controller
    {
        private readonly INSecurity _securityRepository;

        public CustomerController(INSecurity securityRepository)
        {
            _securityRepository = securityRepository;
        }

        private string GetUserType() {
            if (User.Identity.Name != null) {
                List<UserInformation> users = _securityRepository.GetUserInformation(User.Identity.Name);
                if (users.Count > 0) {
                    var user = users[0];
                    return user.user_type;
                }
            }
            return "";
        }

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Customer()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index","Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult EditProfile() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult EditUsers() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult Contact() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult Address() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult changePass() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult billing() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult Shipments() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Customer page.";

            return View();
        }

        public ActionResult Notes(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;

            return View();
        }

        public ActionResult Files(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;

            return View();
        }

        public ActionResult approveQuotes(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;

            return View();
        }

        public ActionResult shipmentTracking(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;

            return View();
        }

        public ActionResult viewShipment(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;

            return View();
        }

        public ActionResult Profile()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "C") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Edit Profile";
            var dataModel = _securityRepository.GetUserList();
            return View(model: dataModel);
        }

        
    }
}
