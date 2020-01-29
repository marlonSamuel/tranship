using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TransShipBO.BO;
using TransShipModel;
using TransShipModel.DTO;
using TransShipModel.Responses;

namespace TransShip.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class UsersController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BOUsers user = new BOUsers();

                var listUsers = new List<users>();
                listUsers = user.GetUsers();
                return Request.CreateResponse(HttpStatusCode.OK, listUsers);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get users - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        //get users for email
        public HttpResponseMessage GetUserEmail(int id) {
            try {

                BOUsers user = new BOUsers();

                var listUsers = new List<users>();
                listUsers = user.GetUserId(id);
                return Request.CreateResponse(HttpStatusCode.OK, listUsers);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get users - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        //get user by idUser
        public HttpResponseMessage getUserByUserId(int userId) {
            try {

                BOUsers User = new BOUsers();

                var user = new users();
                user = User.getUserByUserId(userId);
                return Request.CreateResponse(HttpStatusCode.OK, user);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get user - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(users model) {
            try {
                BOUsers user = new BOUsers();
                users coun;

                coun = user.SaveUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save user - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(users model) {
            try {
                BOUsers user = new BOUsers();
                users coun;

                coun = user.UpdateUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update user - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOUsers user = new BOUsers();

                var delete = user.DeleteUser(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update user - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetUser(string email) {
            try {

                BOUsers user = new BOUsers();

                var User = new Payments();
                User = user.getUser(email);
                return Request.CreateResponse(HttpStatusCode.OK, User);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get user - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage PutProfile(customers model) {
            try {
                BOUsers user = new BOUsers();
                customers coun;

                coun = user.updateProfile(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update profile - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        //update logo of profile
        [HttpPut]
        public HttpResponseMessage UploadLogo() {
            var filePath = "~/Logos/";

            customers customer = new customers();
            customers customers;

            BOUsers user = new BOUsers();

            var logo = HttpContext.Current.Request.Files.AllKeys.Any();

            //get paramaetes shipment files
            var customerId = HttpContext.Current.Request.Params["model.customerId"];

            try {
                if (HttpContext.Current.Request.Files.AllKeys.Any()) {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadLogo"];

                    if (httpPostedFile != null) {
                        var file_name = httpPostedFile.FileName;

                        // Get the complete file path
                        string fileSave = DateTime.Now.ToString("yyyyMMddHHmmss") + file_name;

                        // if the directory doesn't exist - create it. 
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "/" + customerId))) {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "/" + customerId));

                            string path = filePath + "/"+ customerId+ "/" + fileSave;
                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));

                        } else {
                            string path = filePath + "/" + customerId + "/" + fileSave;
                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));
                        }

                        customer.logo = fileSave;
                    }

                    //map de parametes to model shipment attachment
                    customer.customerId = Convert.ToDecimal(customerId);
                }

                BOCustomers custom = new BOCustomers();


                customer.customerId = Convert.ToDecimal(customerId);
                customers = custom.GetCustomerByCustomerId(Convert.ToInt32(customerId));
                customers.logo = customer.logo;
                customers = user.updateProfile(customers);

                return Request.CreateResponse(HttpStatusCode.OK, customers);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update Profile - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        public HttpResponseMessage getPass(int id, string email, string password, string newPass) {
            try {
                BOUsers user = new BOUsers();
                string result = user.changePassword(id, email, password, newPass);

                return Request.CreateErrorResponse(HttpStatusCode.OK,result);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to change password - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

    }
    }