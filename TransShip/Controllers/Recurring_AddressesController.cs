using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TransShipBO.BO;
using TransShipModel;
using TransShipModel.DTO;
using TransShipModel.Responses;

namespace TransShip.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class Recurring_AddressesController : ApiController {
        
        public HttpResponseMessage Get(int idCustomer) {
            try {

               BORecurring_Addresses address = new BORecurring_Addresses();

                var listAddresses = new List<RecurringAddresses>();
                listAddresses = address.GetAddress(idCustomer);
                return Request.CreateResponse(HttpStatusCode.OK, listAddresses);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get recurring addresses - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetOne(int idAddress) {
            try {

                BORecurring_Addresses address = new BORecurring_Addresses();

                var Address = new recurring_addresses();
                Address = address.getOneAddres(idAddress);
                return Request.CreateResponse(HttpStatusCode.OK, Address);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get recurring address - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(recurring_addresses model) {
            try {
                BORecurring_Addresses address = new BORecurring_Addresses();
                recurring_addresses addr;

                addr = address.SaveAddress(model);
                return Request.CreateResponse(HttpStatusCode.OK, addr);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save recurring address - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(recurring_addresses model) {
            try {
                BORecurring_Addresses address = new BORecurring_Addresses();
                recurring_addresses addr;

                addr = address.UpdateAddress(model);
                return Request.CreateResponse(HttpStatusCode.OK, addr);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update recurring address - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int idAddress) {
            try {
                BORecurring_Addresses address = new BORecurring_Addresses();

                var delete = address.DeleteAddress(idAddress);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update contact - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
        
    }
    }