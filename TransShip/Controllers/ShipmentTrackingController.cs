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
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class ShipmentTrackingController : ApiController
    {
        public HttpResponseMessage GetShipmentTracking(int shipmentId) {
            try {

                BOShipmentTracking shipTracking = new BOShipmentTracking();

                var listTracks = new List<Tracking>();
                listTracks = shipTracking.GetShipmentTracking(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listTracks);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get tracking - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Get(int shipmentId) {
            try {

                BOShipmentTracking shipTracking = new BOShipmentTracking();

                var listTracks = new List<shipment_tracking>();
                listTracks = shipTracking.Get(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listTracks);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get tracking - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(shipment_tracking model) {
            try {
                BOShipmentTracking shipTracking = new BOShipmentTracking();
                shipment_tracking ship;

                ship = shipTracking.Save(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save tracking - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(shipment_tracking model) {
            try {
                BOShipmentTracking shipTracking = new BOShipmentTracking();
                shipment_tracking ship;

                ship = shipTracking.update(model);
                return Request.CreateResponse(HttpStatusCode.OK, ship);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update tracking - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(shipment_tracking shipservice) {
            try {
                BOShipmentTracking shipTracking = new BOShipmentTracking();

                var delete = shipTracking.Delete(shipservice);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to delete tracking - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
