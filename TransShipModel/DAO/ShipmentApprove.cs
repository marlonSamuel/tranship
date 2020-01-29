using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using TransShipModel.DTO;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class ShipmentApprove {

        public List<ShipServiceCost> getCostsServicesByShipmentId(int shipmentId) {
            var list = new List<ShipServiceCost>();
            ShipServiceCost shipservice;
            decimal totalServices;
            try {
                 using (var ctx = new transshipEntities()) {
                    var query = (from ss in ctx.shipment_services
                                 join st in ctx.service_types on ss.service_typeId equals st.service_typeId
                                 join su in ctx.measure_units on ss.measure_unitId equals su.measure_unitId
                                 where ss.shipmentId == shipmentId
                                 select new {
                                     shipment_id = ss.shipmentId,
                                     service_typeId = ss.service_typeId,
                                     measure_unitId = ss.measure_unitId,
                                     quantity = ss.quantity,
                                     estimated_unit_price = ss.estimated_unit_price,
                                     confimated_unit_price = ss.confirmed_unit_price,
                                     type = st.descripcion,
                                     description = su.description,
                                     totalbyreg = ss.quantity * (ss.confirmed_unit_price ?? 0),
                                 }).ToList();

                    foreach (var s in query) {
                        shipservice = new ShipServiceCost();
                        shipservice.shipmentId = s.shipment_id;
                        shipservice.service_typeId = s.service_typeId;
                        shipservice.measure_unitId = s.measure_unitId;
                        shipservice.quantity = s.quantity;
                        shipservice.estimated_unit_price = s.estimated_unit_price;
                        shipservice.confirmed_unit_price = s.confimated_unit_price;
                        shipservice.type = s.type;
                        shipservice.description = s.description;
                        shipservice.totalbyreg = s.totalbyreg;
                        totalServices = shipservice.totalServices + s.totalbyreg;
                        shipservice.totalServices = totalServices;
                        list.Add(shipservice);
                    }
                    
                    return list;
            } 
           }catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public List<ShipCosts> getCostsByShipmentId(int shipmentId) {
            var list = new List<ShipCosts>();
            ShipCosts shipcost;
            decimal? totalCosts = 0;
            try {
                 using (var ctx = new transshipEntities()) {
                    var query = (from ss in ctx.shipment_cost
                                 join st in ctx.service_types on ss.service_typeId equals st.service_typeId
                                 join su in ctx.measure_units on ss.measure_unitId equals su.measure_unitId
                                 where ss.shipmentId == shipmentId
                                 select new {
                                     shipment_id = ss.shipmentId,
                                     service_typeId = ss.service_typeId,
                                     measure_unitId = ss.measure_unitId,
                                     quantity = ss.quantity,
                                     vendor = ss.vendor,
                                     unit_price = ss.unit_price,
                                     type = st.descripcion,
                                     description = su.description,
                                     totalbyreg = ss.quantity * ss.unit_price,
                                 }).ToList();
              
                    foreach (var s in query) {
                        shipcost = new ShipCosts();
                        shipcost.shipmentId = s.shipment_id;
                        shipcost.service_typeId = s.service_typeId;
                        shipcost.measure_unitId = s.measure_unitId;
                        shipcost.quantity = s.quantity;
                        shipcost.unit_price = s.unit_price;
                        shipcost.type = s.type;
                        shipcost.vendor = s.vendor;
                        shipcost.description = s.description;
                        shipcost.totalbyreg = s.totalbyreg;
                        totalCosts= totalCosts + s.totalbyreg;
                        shipcost.totalCosts = totalCosts;
                        list.Add(shipcost);
                    }
                    
                    return list;
            } 
           }catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
    }
}