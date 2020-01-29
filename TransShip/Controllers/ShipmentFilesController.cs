using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TransShipBO.BO;
using TransShipModel;
using TransShipModel.DTO;
using TransShipModel.Responses;

namespace TransShip.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class ShipmentFilesController : ApiController
    {
        public HttpResponseMessage Get(int shipmentId) {
            try {

                BOShipmentFiles file = new BOShipmentFiles();

                var listFiles = new List<Files>();
                listFiles = file.getShipmentFiles(shipmentId);
                return Request.CreateResponse(HttpStatusCode.OK, listFiles);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get shipment files - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }


        [HttpPost]
        public HttpResponseMessage UploadFile() {
            var filePath = "~/shipmentUpload/";

            shipment_attachment shipfile = new shipment_attachment();
            shipment_attachment shipmentFile;
            BOShipmentFiles fileShip = new BOShipmentFiles();

            var file = HttpContext.Current.Request.Files.AllKeys.Any();

            //get paramaetes shipment files
            var shipmentId = HttpContext.Current.Request.Params["model.shipmentId"];
            var file_typeId = HttpContext.Current.Request.Params["model.file_typeId"];
            var userId = HttpContext.Current.Request.Params["model.userId"];
            var description = HttpContext.Current.Request.Params["model.description"];
            var customerId = HttpContext.Current.Request.Params["model.customerId"];
            //var file_name = HttpContext.Current.Request.Params["model.file_name"];

            try {
                if (HttpContext.Current.Request.Files.AllKeys.Any()) {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];

                    if (httpPostedFile != null) {
                        var file_name = httpPostedFile.FileName;

                        // Get the complete file path
                        string fileSave = DateTime.Now.ToString("yyyyMMddHHmmss") + file_name;

                        // if the \pix directory doesn't exist - create it. 
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "/" + customerId))) {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "/" + customerId));
                        }

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "/" + customerId + "/" + shipmentId))) {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "/" + customerId + "/" + shipmentId));

                            string path = filePath + "/" + customerId + "/" + shipmentId +"/" + fileSave;

                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));
                        } else {
                            string path = filePath + "/" + customerId + "/" + shipmentId + "/" + fileSave;
                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));
                        }

                        //var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/shipmentUpload"),httpPostedFile.FileName);

                        // Save the uploaded file to "UploadedFiles" folder
                        //httpPostedFile.SaveAs(fileSavePath);

                        shipfile.file_name = fileSave;
                    }

                    //map de parametes to model shipment attachment
                    shipfile.shipmentId = Convert.ToDecimal(shipmentId);
                    shipfile.userId = Convert.ToDecimal(userId);
                    shipfile.file_typeId = Convert.ToDecimal(file_typeId);
                    shipfile.description = description;
                    shipfile.creation_date = DateTime.Today;
                }

                shipmentFile = fileShip.saveShipmentFile(shipfile);
                return Request.CreateResponse(HttpStatusCode.OK, shipmentFile);
            } catch(Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save shipment file - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }
        
    }
}
