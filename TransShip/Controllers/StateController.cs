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
    public class StateController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get(int id) {
            try {

                BOState stat = new BOState();

                var listStates = new List<state>();
                listStates = stat.GetStates(id);
                return Request.CreateResponse(HttpStatusCode.OK, listStates);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get States - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Post(state model) {
            try {
                BOState stat = new BOState();
                state state;

                state = stat.SaveState(model);
                return Request.CreateResponse(HttpStatusCode.OK, state);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save states - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(state model) {
            try {
                BOState stat = new BOState();
                state state;

                state = stat.UpdateUnit(model);
                return Request.CreateResponse(HttpStatusCode.OK, state);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update measure - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(string id) {
            try {
                BOState stat = new BOState();

                var delete = stat.DeleteState(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to delete state - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
