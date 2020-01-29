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
using TransShipModel.DTO;

namespace TransShip.Controllers
{
    public class ShipmentApproveController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage GetShipServiceCost(int shipmentId) {
            try {

                BOShipmentApprove servicecost = new BOShipmentApprove();

                var listServicesCosts = new List<ShipServiceCost>();
                listServicesCosts = servicecost.getCostsServicesByShipmentId(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listServicesCosts);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get services costs - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetShipCost(int shipmentId) {
            try {

                BOShipmentApprove servicecost = new BOShipmentApprove();

                var listShipCosts = new List<ShipCosts>();
                listShipCosts = servicecost.getCostsByShipmentId(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listShipCosts);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get shipment cost - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
