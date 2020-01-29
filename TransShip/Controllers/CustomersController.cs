using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TransShipBO.BO;
using TransShipModel;
using TransShipModel.Responses;

namespace TransShip.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class CustomersController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BOCustomers customer = new BOCustomers();

                var listCustomers = new List<customers>();
                listCustomers = customer.GetCustomers();
                return Request.CreateResponse(HttpStatusCode.OK, listCustomers);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get customers - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetCustomerByCustomerId(int customerId) {
            try {

                BOCustomers customer = new BOCustomers();

                var customers = new customers();
                customers = customer.GetCustomerByCustomerId(customerId);
                return Request.CreateResponse(HttpStatusCode.OK, customers);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get customers - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(customers model) {
            try {
                BOCustomers customer = new BOCustomers();
                customers cont;

                cont = customer.SaveCustomer(model);
                return Request.CreateResponse(HttpStatusCode.OK, cont);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save customer - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(customers model) {
            try {
                BOCustomers customer = new BOCustomers();
                customers cont;

                cont = customer.UpdateCustomer(model);
                return Request.CreateResponse(HttpStatusCode.OK, cont);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update customers - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int customerId) {
            try {
                BOCustomers customer = new BOCustomers();

                var delete = customer.DeleteCustomer(customerId);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update customer - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
        
    }
    }