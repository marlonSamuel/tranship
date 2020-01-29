using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class ShipmentServices {

        public List<shipment_services> getShipmentServices(int shipmentId) {
            var list = new List<shipment_services>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipment_services.Where(x => x.shipmentId == shipmentId).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public void setVendorPrices(int shipmentId) {
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var result = ctx.Database.SqlQuery <List<string>>("sp_suggest_shipment_price "+shipmentId).ToList();

                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_services SaveShipmentService(shipment_services model) {

            var ShipService = new shipment_services();
            model.estimated_unit_price = 0;
            try {
                using (var ctx = new transshipEntities()) {
                    ShipService = ctx.shipment_services.Add(model);
                    ctx.SaveChanges();

                    return ShipService;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_services updateShipmentService(shipment_services model) {
            shipment_services shipServ = new shipment_services();

            shipServ = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(shipServ).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return shipServ;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string Delete(shipment_services shipservice) {

            string rpta;
            shipment_services shipServ = new shipment_services();
            try {
                using (var ctx = new transshipEntities()) {
                    shipServ = ctx.shipment_services.FirstOrDefault(x => x.shipmentId == shipservice.shipmentId 
                                                                      && x.service_typeId == shipservice.service_typeId
                                                                      && x.measure_unitId ==  shipservice.measure_unitId);

                    if (shipServ != null) {

                        ctx.Entry(shipServ).State = EntityState.Deleted;
                        ctx.SaveChanges();

                        rpta = "ok";

                    } else {
                        rpta = "fail";
                    }

                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }

            return rpta;
        }

    }
}