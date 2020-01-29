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
using TransShipModel.Responses;

namespace TransShip.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class VendorsController : ApiController {
        
        public HttpResponseMessage Get() {
            try {

               BOVendors vendors = new BOVendors();

                var listVendors = new List<vendors>();
                listVendors = vendors.GetVendors();
                return Request.CreateResponse(HttpStatusCode.OK, listVendors);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get vendors - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetVendorById(int vendorId) {
            try {

                BOVendors vendors = new BOVendors();

                var vendor = new vendors();
                vendor = vendors.getVendorById(vendorId);
                return Request.CreateResponse(HttpStatusCode.OK, vendor);

            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to get vendors - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Post(vendors model) {
            try {
                BOVendors vendors = new BOVendors();
                vendors coun;

                coun = vendors.SaveVendors(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to save vendors - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Put(vendors model) {
            try {
                BOVendors vendors = new BOVendors();
                vendors coun;

                coun = vendors.UpdateVendors(model);
                return Request.CreateResponse(HttpStatusCode.OK, coun);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update vendors - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        //update logo vendor
        [HttpPut]
        public HttpResponseMessage UploadLogo() {
            var filePath = "~/Vendors/";

            vendors vendor = new vendors();
            vendors vend;

            BOVendors vendors = new BOVendors();

            var logo = HttpContext.Current.Request.Files.AllKeys.Any();

            //get paramaetes shipment files
            var vendorId = HttpContext.Current.Request.Params["model.vendorId"];

            try {
                if (HttpContext.Current.Request.Files.AllKeys.Any()) {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadLogo"];

                    if (httpPostedFile != null) {
                        var file_name = httpPostedFile.FileName;

                        // Get the complete file path
                        string fileSave = DateTime.Now.ToString("yyyyMMddHHmmss") + file_name;

                        // if the directory doesn't exist - create it. 
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "/" + vendorId))) {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "/" + vendorId));

                            string path = filePath + "/" + vendorId + "/" + fileSave;
                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));

                        } else {
                            string path = filePath + "/" + vendorId + "/" + fileSave;
                            httpPostedFile.SaveAs(HttpContext.Current.Server.MapPath(path));
                        }

                        vendor.logo = fileSave;
                    }

                    //map de parametes to model shipment attachment
                    vendor.vendorId = Convert.ToDecimal(vendorId);
                }
                
                vendor.vendorId = Convert.ToDecimal(vendorId);
                vend = vendors.getVendorById(Convert.ToInt32(vendorId));
                vend.logo = vendor.logo;
                vend = vendors.UpdateVendors(vend);

                return Request.CreateResponse(HttpStatusCode.OK, vend);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update Profile - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOVendors vendors = new BOVendors();

                var delete = vendors.DeleteVendors(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage message = new ErrorMessage("2.1", "Exception to update vendors - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

        }


    }
    }