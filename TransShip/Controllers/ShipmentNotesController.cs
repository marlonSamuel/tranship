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
    public class ShipmentNotesController : ApiController
    {
        public HttpResponseMessage Get(int shipmentId) {
            try {

                BOShipmentNotes shipNote = new BOShipmentNotes();

                var listNotes = new List<Notes>();
                listNotes = shipNote.getShipNotes(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listNotes);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipment notes - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(shipment_notes model) {
            try {
                BOShipmentNotes shipNote = new BOShipmentNotes();
                shipment_notes note;

                note = shipNote.saveShipmentNote(model);
                return Request.CreateResponse(HttpStatusCode.OK, note);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save shipment note - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(shipment_notes model) {
            try {
                BOShipmentNotes shipNote = new BOShipmentNotes();
                shipment_notes note;

                note = shipNote.updateNote(model);
                return Request.CreateResponse(HttpStatusCode.OK, note);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update shipment note - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
