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
    public class RatesController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BORates rates = new BORates();

                var listRates = new List<rates>();
                listRates = rates.GetRates();
                return Request.CreateResponse(HttpStatusCode.OK, listRates);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get rates - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetByVendorId(int vendorId) {
            try {

               BORates rates = new BORates();

                var listRates = new List<rates>();
                listRates = rates.GetRates().FindAll(x => x.vendorId == vendorId);
                return Request.CreateResponse(HttpStatusCode.OK, listRates);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception get rate - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetRatesByVendorId(int rateVendorId) {
            try {

               BORates rates = new BORates();

                var listRates = new List<RatesObject>();
                listRates = rates.GetRatesVendor(rateVendorId);
                return Request.CreateResponse(HttpStatusCode.OK, listRates);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception get rate - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }
        public HttpResponseMessage Post(rates model) {
            try {
                BORates rates = new BORates();
                rates coun;

                coun = rates.SaveRates(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save rates - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(rates model) {
            try {
                BORates rates = new BORates();
                rates coun;

                coun = rates.UpdateRates(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update rates - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(rates model) {
            try {
                BORates rates = new BORates();
                
                var delete = rates.DeleteRates(model);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update rates - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

    }
    }