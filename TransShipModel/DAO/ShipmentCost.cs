using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class ShipmentCost {

        public List<shipment_cost> get(int shipmentId) {
            var list = new List<shipment_cost>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipment_cost.Where(x => x.shipmentId == shipmentId).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_cost Save(shipment_cost model) {

            var shipCost = new shipment_cost();
            try {
                using (var ctx = new transshipEntities()) {
                    shipCost = ctx.shipment_cost.Add(model);
                    ctx.SaveChanges();

                    return shipCost;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_cost update(shipment_cost model) {
            shipment_cost shipCost = new shipment_cost();

            shipCost = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(shipCost).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return shipCost;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string Delete(shipment_cost shipservice) {

            string rpta;
            shipment_cost shipCost = new shipment_cost();
            try {
                using (var ctx = new transshipEntities()) {
                    shipCost = ctx.shipment_cost.FirstOrDefault(x => x.shipmentId == shipservice.shipmentId 
                                                                      && x.service_typeId == shipservice.service_typeId
                                                                      && x.measure_unitId ==  shipservice.measure_unitId);

                    if (shipCost != null) {

                        ctx.Entry(shipCost).State = EntityState.Deleted;
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