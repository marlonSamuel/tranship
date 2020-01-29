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
    public class ShipmentCostController : ApiController
    {
        public HttpResponseMessage Get(int shipmentId) {
            try {

                BOShipmentCost shipCost = new BOShipmentCost();

                var listCosts = new List<shipment_cost>();
                listCosts = shipCost.Get(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listCosts);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get contacts - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(shipment_cost model) {
            try {
                BOShipmentCost shipCost = new BOShipmentCost();
                shipment_cost ship;

                ship = shipCost.Save(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save shipment service - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(shipment_cost model) {
            try {
                BOShipmentCost shipCost = new BOShipmentCost();
                shipment_cost ship;

                ship = shipCost.update(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipment service - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(shipment_cost shipservice) {
            try {
                BOShipmentCost shipCost = new BOShipmentCost();

                var delete = shipCost.Delete(shipservice);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update contact - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
