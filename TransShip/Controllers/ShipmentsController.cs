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
using Newtonsoft.Json;
using TransShipModel.DTO;

namespace TransShip.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class ShipmentsController : ApiController
    {
        public HttpResponseMessage Get(int idCustomer) {
            try {

                BOShipments shipment = new BOShipments();

                var listShipments = new List<shipments>();
                listShipments = shipment.getShipment(idCustomer);
                return Request.CreateResponse(HttpStatusCode.OK, listShipments);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetByStatus(string status) {
            try {

                BOShipments shipment = new BOShipments();

                var listShipments = new List<shipments>();
                listShipments = shipment.getShipmentByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, listShipments);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetByStatusAndDateRange(string status, DateTime from, DateTime to) {
            try {

                BOShipments shipment = new BOShipments();

                var listShipments = new List<shipments>();
                listShipments = shipment.getShipmentByStatusAndDateRange(status, from, to);
                return Request.CreateResponse(HttpStatusCode.OK, listShipments);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage getLastTwoShipment(int customerId, string state) {
            try {

                BOShipments shipment = new BOShipments();

                var listShipments = new List<shipmentHistory>();
                listShipments = shipment.getLastTwoShipment(customerId, state);
                return Request.CreateResponse(HttpStatusCode.OK, listShipments);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetOne(int shipmentId) {
            try {

                BOShipments Shipment = new BOShipments();

                var shipment = new shipments();
                shipment = Shipment.getOneShipment(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, shipment);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipment - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage GetByPO(int customerId, string PO) {
            try {

                BOShipments shipment = new BOShipments();

                var Shipments = new shipments();
                Shipments = shipment.getShipmentByPO(customerId, PO);
                return Request.CreateResponse(HttpStatusCode.OK, Shipments);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(shipments model) {
            try {
                BOShipments shipment = new BOShipments();
                shipments ship;

                ship = shipment.saveShipment(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(shipments model) {
            try {
                BOShipments shipment = new BOShipments();
                shipments ship;

                ship = shipment.updateShipments(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage SetStatus(int shipmentId, string status) {
            try {
                BOShipments shipment = new BOShipments();
                shipments ship = shipment.getOneShipment(shipmentId);
                ship.status = status;
                ship = shipment.updateShipments(ship);
                return Request.CreateResponse(HttpStatusCode.OK, 1);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOShipments shipment = new BOShipments();

                var delete = shipment.deleteShipment(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipment - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        [Serializable]
        class Result {
            public string result { get; set; }
            public string message { get; set; }
            public string sendMail { get; set; }
        }

        public HttpResponseMessage GetCheckShipmentsByMembership(int customerId) {
            
            try {

                BOShipments shipment = new BOShipments();

                string result = shipment.checkShipmentsByMembership(customerId);
                

                Result json  = JsonConvert.DeserializeObject<Result>(result);
                if (json.sendMail == "Yes") {
                     
                    BOContacts contact = new BOContacts();
                    var listContacts = new List<customer_contacts>();
                    listContacts = contact.GetContact(customerId);
                    //List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList();
                    var emailhandler = new TransShip.EMailHandler.EMailHandler();

                    emailhandler.SendMembershipEmail(json.message, listContacts[0].email);
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipments - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

    }
}
