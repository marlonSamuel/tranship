using System.Web.Mvc;
using TransShip.Models;
using TransShipModel.Abstract;
using TransShip.LogBook;
using System.Web.Security;
using TransShipBO.BO;
using TransShipModel.DTO;
using System.Collections.Generic;

namespace TransShip.Controllers {
    public class HomeController : Controller {

        private readonly INSecurity _securityRepository;

        public HomeController(INSecurity securityRepository)
        {
            _securityRepository = securityRepository;
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();            
            return RedirectToRoute("Default");
        }        

        [HttpGet]
        public ActionResult Index(string apiInformation) {
            ViewData["user"] = null;
            ViewData["customerId"] = null;
            if (User.Identity.Name != null) {
                List<UserInformation> users = _securityRepository.GetUserInformation(User.Identity.Name);
                if (users.Count > 0) {
                    ViewData["user"] = users[0];
                    ViewData["customerId"] = users[0].customerId;
                }
            }
            if (string.IsNullOrEmpty(apiInformation))
            {                
                return View();
            }
            else
            {
                var result = _securityRepository.ConfirmUserName(apiInformation);
                ViewData["result"] = string.IsNullOrEmpty(result) ? string.Empty : "Account confirmed!";
                return View();
            }            
        }

        [HttpGet]
        public ActionResult RecoverPassword(string apiInformation)
        {
            LogBook.LogBook.TextLog.Info(string.Format("API Key Information {0}", apiInformation));
            if (string.IsNullOrEmpty(apiInformation) == false)
            {
                var result = _securityRepository.ConfirmUserName(apiInformation);
                ViewData["apiInformation"] = apiInformation;
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario model, string returnUrl)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Usuario Ingresado {0}", model.Email));
            LogBook.LogBook.TextLog.Info(string.Format("Clave Ingresada {0}", model.Password));
            var result = _securityRepository.CheckUserName(model.Email,model.Password);

            if (result) {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                ViewData["Message"] = "Login successful";
            } else {
                ViewData["Message"] = "Invalid credentials";
                return View();
            }
            return RedirectToRoute("Default");
        }

        public ActionResult Register() {
            ViewBag.Message = "Register";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}