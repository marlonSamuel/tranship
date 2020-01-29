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


namespace TransShip.Controllers
{
    public class MemberShip_PaymentController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get() {
            try {

                BOMemberShip_Payments customer = new BOMemberShip_Payments();

                var listCustomer = new List<Customers_view>();
                listCustomer = customer.GetCustomers();
                return Request.CreateResponse(HttpStatusCode.OK, listCustomer);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get memberShip levels - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetHistory(int idCustomer) {
            try {

                BOMemberShip_Payments history = new BOMemberShip_Payments();

                var listCustomer = new List<History>();
                listCustomer = history.getHistory(idCustomer);
                return Request.CreateResponse(HttpStatusCode.OK, listCustomer);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get memberShip history - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }



        public HttpResponseMessage Post(membership_history model) {
            try {
                BOMemberShip_Payments member = new BOMemberShip_Payments();
                membership_history memb;

                memb = member.saveHistory(model);
                return Request.CreateResponse(HttpStatusCode.OK, memb);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save history - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(membership_history model) {
            try {
                BOMemberShip_Payments member = new BOMemberShip_Payments();
                membership_history memb;

                memb = member.updtaHistory(model);
                return Request.CreateResponse(HttpStatusCode.OK, memb);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update memberShip history - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOMemberShip_Payments member = new BOMemberShip_Payments();

                var delete = member.DeleteFile(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update memberShip history - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
