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

namespace TransShip.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class ShipmetenServiceController : ApiController
    {
        public HttpResponseMessage Get(int shipmentId) {
            try {

                BOShipmentService ShipServ = new BOShipmentService();

                var listServices = new List<shipment_services>();
                listServices = ShipServ.GetServicesShipment(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listServices);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get contacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(shipment_services model) {
            try {
                BOShipmentService ShipServ = new BOShipmentService();
                shipment_services ship;

                ship = ShipServ.SaveShipmentService(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save shipment service - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage SetVendorPrices(int shipmentId) {
            try {
                BOShipmentService ShipServ = new BOShipmentService();
                
                ShipServ.SetVendorPrices(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to set vendor prices - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(shipment_services model) {
            try {
                BOShipmentService ShipServ = new BOShipmentService();
                shipment_services ship;

                ship = ShipServ.updateShipmentService(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipment service - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(shipment_services shipservice) {
            try {
                BOShipmentService shipServ = new BOShipmentService();

                var delete = shipServ.Delete(shipservice);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update contact - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
