using System.Web.Mvc;
using TransShip.Models;
using TransShipModel.Abstract;
using TransShip.LogBook;
using System.Web.Security;
using TransShipModel.DTO;
using System.Collections.Generic;

namespace TransShip.Controllers
{
    public class AdminController : Controller
    {
        private readonly INSecurity _securityRepository;



        public AdminController(INSecurity securityRepository)
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
            if (string.IsNullOrEmpty(User.Identity.Name)) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Admin()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            
            ViewData["Message"] = "Admin page.";

            return View();
        }

        public ActionResult Users()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = _securityRepository.GetUserList();
            return View(model: dataModel);
        }

        public ActionResult EditUser() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = _securityRepository.GetUserList();
            return View(model: dataModel);
        }

        public ActionResult changePass() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";

            return View();
        }

        public ActionResult Countries()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = _securityRepository.GetCountryList();
            return View(model: dataModel);
        }

        public ActionResult Countrie() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = "hola";
            return View(model: dataModel);
        }

        public ActionResult File_Type() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = "hola";
            return View(model: dataModel);
        }

        public ActionResult Service_Type() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = "hola";
            return View(model: dataModel);
        }

        public ActionResult Customers() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = "hola";
            return View(model: dataModel);
        }

        public ActionResult MemberShip() {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = "hola";
            return View(model: dataModel);
        }

        public ActionResult AproveRegister()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Admin page.";
            var dataModel = _securityRepository.GetPendingCustomers();
            return View(model: dataModel);
        }
        
        public ActionResult Vendors()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Vendors";
            
            return View();
        }

        public ActionResult Files(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "Files";

            return View();
        }

        public ActionResult Notes(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "View Shipment";

            return View();
        }

        public ActionResult ViewShipment(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "View Shipment";
            
            return View();
        }

        public ActionResult CancelShipment(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "Cancel Shipment";
            
            return View();
        }

        public ActionResult DeliveryShipment(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "Delivery Shipment";

            return View();
        }

        public ActionResult ShipmentTracking(int shipmentId) {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["shipmentId"] = shipmentId;
            ViewData["Message"] = "Shipment Tracking";
            
            return View();
        }

        public ActionResult Shipments()
        {
            if (string.IsNullOrEmpty(User.Identity.Name) || GetUserType() != "T") {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Shipments";
            
            return View();
        }
    }
}
