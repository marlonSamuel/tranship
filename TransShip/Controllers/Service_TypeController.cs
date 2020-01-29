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
    public class Service_TypeController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get() {
            try {

                BOService_types service = new BOService_types();

                var listServices = new List<service_types>();
                listServices = service.GetServices();
                return Request.CreateResponse(HttpStatusCode.OK, listServices);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get services types - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Post(service_types model) {
            try {
                BOService_types service = new BOService_types();
                service_types fileService;

                fileService = service.SaveService(model);
                return Request.CreateResponse(HttpStatusCode.OK, fileService);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save service type - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(service_types model) {
            try {
                BOService_types service = new BOService_types();
                service_types serviceType;

                serviceType = service.UpdateService(model);
                return Request.CreateResponse(HttpStatusCode.OK, serviceType);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update service type - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOService_types service = new BOService_types();

                var delete = service.DeleteService(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update service type - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
