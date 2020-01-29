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
    public class VendorContactsController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BOVendorContacts vendorContacts = new BOVendorContacts();

                var listVendorContacts = new List<vendor_contacts>();
                listVendorContacts = vendorContacts.GetVendorContacts();
                return Request.CreateResponse(HttpStatusCode.OK, listVendorContacts);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get vendorContacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetByVendorId(int vendorId) {
            try {

               BOVendorContacts vendorContacts = new BOVendorContacts();

                var listVendorContacts = new List<vendor_contacts>();
                listVendorContacts = vendorContacts.GetVendorContacts().FindAll(x => x.vendorId == vendorId);
                return Request.CreateResponse(HttpStatusCode.OK, listVendorContacts);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get vendorContacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Post(vendor_contacts model) {
            try {
                BOVendorContacts vendorContacts = new BOVendorContacts();
                vendor_contacts vc;

                vc = vendorContacts.SaveVendorContacts(model);
                return Request.CreateResponse(HttpStatusCode.OK, vc);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save vendorContacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(vendor_contacts model) {
            try {
                BOVendorContacts vendorContacts = new BOVendorContacts();
                var save = vendorContacts.UpdateVendorContacts(model);
                return Request.CreateResponse(HttpStatusCode.OK, save);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update vendorContacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(int vendorId, int contactId) {
            try {
                BOVendorContacts vendorContacts = new BOVendorContacts();
                var model = new vendor_contacts();
                model = vendorContacts.GetVendorContacts().First(x => (x.vendorId == vendorId && x.contactId == contactId));
                var delete = vendorContacts.DeleteVendorContacts(model);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update vendorContacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

    }
}