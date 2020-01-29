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
    public class UnitController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get() {
            try {

                BOMeasure_Unit unit = new BOMeasure_Unit();

                var listUnits = new List<measure_units>();
                listUnits = unit.Get();
                return Request.CreateResponse(HttpStatusCode.OK, listUnits);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get services measure unit - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        public HttpResponseMessage GetByServiceId(int id) {
            try {

                BOMeasure_Unit unit = new BOMeasure_Unit();

                var units = new List<measure_units>();
                units = unit.GetUnitByService(id);
                return Request.CreateResponse(HttpStatusCode.OK, units);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get service types - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }
        }


        public HttpResponseMessage Post(measure_units model) {
            try {
                BOMeasure_Unit unit = new BOMeasure_Unit();
                measure_units measureU;

                measureU = unit.SaveUnit(model);
                return Request.CreateResponse(HttpStatusCode.OK, measureU);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save service measures - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(measure_units model) {
            try {
                BOMeasure_Unit unit = new BOMeasure_Unit();
                measure_units measureU;

                measureU = unit.UpdateUnit(model);
                return Request.CreateResponse(HttpStatusCode.OK, measureU);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update measure - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOMeasure_Unit unit = new BOMeasure_Unit();

                var delete = unit.DeleteUnit(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update measure - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
