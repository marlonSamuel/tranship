using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class Shipments {

        public List<shipments> getShipments(int idCustomer) {
            var list = new List<shipments>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipments.Where(x=>x.customerId == idCustomer).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public List<shipments> getShipmentsByStatus(string status) {
            var list = new List<shipments>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipments.Where(x=>x.status == status).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipments getShipmentsByPO(int customerId, string PO) {
            var shipment = new shipments();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    shipment = ctx.shipments.Where (x => x.customerId == customerId && x.purcharse_order == PO).FirstOrDefault();

                    return shipment;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public List<shipments> getShipmentsByStatusAndDateRange(string status, DateTime fromD, DateTime toD) {
            var list = new List<shipments>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipments.Where(x=>x.status == status && x.shipment_date >= fromD && x.shipment_date <= toD).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public List<shipmentHistory> getLastTwoShipment(int customerId, string state) {
            var list = new List<shipmentHistory>();
            shipmentHistory shipment_h;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from s in ctx.shipments
                                 join st in ctx.service_types on s.service_typeId equals st.service_typeId
                                 where s.customerId == customerId && s.state == state && s.status == "D"
                                 select new {
                                     shipmentId = s.shipmentId,
                                     customerId = s.customerId,
                                     serviceTypeId = s.service_typeId,
                                     purcharse_order = s.purcharse_order,
                                     consignee_name = s.consignee_name,
                                     shipment_date = s.shipment_date,
                                     description = st.descripcion,
                                     state = s.state,
                                     status = s.status
                                 }).OrderByDescending(x=>x.shipment_date).Take(2).ToList();

                    foreach (var p in query) {
                        shipment_h = new shipmentHistory();
                        shipment_h.shipmentId = p.shipmentId;
                        shipment_h.customerId = p.customerId;
                        shipment_h.serviceTypeId = p.serviceTypeId;
                        shipment_h.purcharse_order = p.purcharse_order;
                        shipment_h.consignee_name = p.consignee_name;
                        shipment_h.shipment_date = p.shipment_date;
                        shipment_h.description = p.description;
                        shipment_h.state = p.state;
                        shipment_h.status = p.status;

                        list.Add(shipment_h);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }

        }

        public shipments getOneShipment(int shipmentId) {
            var shipment = new shipments();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    shipment = ctx.shipments.Where(x => x.shipmentId == shipmentId).SingleOrDefault();

                    return shipment;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipments saveShipment(shipments model) {

            model.tnc_accepted = "N";
            model.status = "N";

            var shipment = new shipments();
            try {
                using (var ctx = new transshipEntities()) {

                    shipment = ctx.shipments.Add(model);
                    ctx.SaveChanges();

                    return shipment;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipments updateShipment(shipments model) {
            shipments shipment = new shipments();

            shipment = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(shipment).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return shipment;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string deleteShipment(int id) {

            string rpta;
            shipments shipment = new shipments();
            try {
                using (var ctx = new transshipEntities()) {
                    shipment = ctx.shipments.FirstOrDefault(x => x.shipmentId == id);

                    if (shipment != null) {

                        ctx.Entry(shipment).State = EntityState.Deleted;
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

        public string checkShipmentsByMembership(int customerId) {
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var result = ctx.Database.SqlQuery <string>("select dbo.fn_check_shipments_by_membership("+customerId+") as result").FirstOrDefault<string>();
                    return result;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
    }
}