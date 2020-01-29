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
    public class Type_FileController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get() {
            try {

                BOFile_types file = new BOFile_types();

                var listFiles = new List<file_types>();
                listFiles = file.GetFiles();
                return Request.CreateResponse(HttpStatusCode.OK, listFiles);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get files - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Post(file_types model) {
            try {
                BOFile_types file = new BOFile_types();
                file_types fileType;

                fileType = file.SaveFile(model);
                return Request.CreateResponse(HttpStatusCode.OK, fileType);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save file - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(file_types model) {
            try {
                BOFile_types file = new BOFile_types();
                file_types fileType;

                fileType = file.UpdateFile(model);
                return Request.CreateResponse(HttpStatusCode.OK, fileType);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update file - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOFile_types file = new BOFile_types();

                var delete = file.DeleteFile(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update file - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
