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

namespace TransShip.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class CuntriesController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BOCountries countrie = new BOCountries();

                var listCountries = new List<countries>();
                listCountries = countrie.GetCountries();
                return Request.CreateResponse(HttpStatusCode.OK, listCountries);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get countries - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(countries model) {
            try {
                BOCountries countrie = new BOCountries();
                countries coun;

                coun = countrie.SaveCountris(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save countrie - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(countries model) {
            try {
                BOCountries countrie = new BOCountries();
                countries coun;

                coun = countrie.UpdateCountris(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update countrie - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOCountries countrie = new BOCountries();

                var delete = countrie.DeleteCountries(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update countrie - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
        
    }
    }