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
    public class ContactsController : ApiController {
        
        public HttpResponseMessage Get(int idCustomer) {
            try {

               BOContacts contact = new BOContacts();

                var listContacts = new List<customer_contacts>();
                listContacts = contact.GetContact(idCustomer);
                return Request.CreateResponse(HttpStatusCode.OK, listContacts);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get contacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(customer_contacts model) {
            try {
                BOContacts contact = new BOContacts();
                customer_contacts cont;

                cont = contact.SaveContact(model);
                return Request.CreateResponse(HttpStatusCode.OK, cont);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save contact - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(customer_contacts model) {
            try {
                BOContacts contact = new BOContacts();
                customer_contacts cont;

                cont = contact.UpdateContact(model);
                return Request.CreateResponse(HttpStatusCode.OK, cont);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update contacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int idContact) {
            try {
                BOContacts contact = new BOContacts();

                var delete = contact.DeleteContact(idContact);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update contact - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
        
    }
    }