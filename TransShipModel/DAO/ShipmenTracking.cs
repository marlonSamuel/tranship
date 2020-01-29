using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class ShipmentTracking {


        public List<Tracking> getShipTracking(int shipmentId) {

            var list = new List<Tracking>();
            Tracking tracking;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from sn in ctx.shipment_tracking
                                 join u in ctx.users on sn.userId equals u.userId
                                 where sn.shipmentId == shipmentId
                                 orderby sn.creation_date descending
                                 select new {
                                     username = u.email,
                                     userId = u.userId,
                                     trackingId = sn.trackingId,
                                     comentaries = sn.comentaries,
                                     status = sn.status,
                                     creation_date = sn.creation_date,
                                     shipmentId = sn.shipmentId
                                 }).ToList();

                    foreach (var n in query) {
                        tracking = new Tracking();
                        tracking.username = n.username;
                        tracking.userId = n.userId;
                        tracking.trackingId = n.trackingId;
                        tracking.comentaries = n.comentaries;
                        tracking.status = n.status;
                        tracking.creation_date = n.creation_date;
                        tracking.shipmentId = n.shipmentId;
                        list.Add(tracking);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
        public List<shipment_tracking> get(int shipmentId) {
            var list = new List<shipment_tracking>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.shipment_tracking.Where(x => x.shipmentId == shipmentId).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_tracking Save(shipment_tracking model) {

            var shipTrack = new shipment_tracking();
            try {
                using (var ctx = new transshipEntities()) {
                    shipTrack = ctx.shipment_tracking.Add(model);
                    ctx.SaveChanges();

                    return shipTrack;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_tracking update(shipment_tracking model) {
            shipment_tracking shipTrack = new shipment_tracking();

            shipTrack = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(shipTrack).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return shipTrack;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string Delete(shipment_tracking tracking) {

            string rpta;
            shipment_tracking shipTrack = new shipment_tracking();
            try {
                using (var ctx = new transshipEntities()) {
                    shipTrack = ctx.shipment_tracking.FirstOrDefault(x => x.shipmentId == tracking.shipmentId 
                                                                      && x.trackingId == tracking.trackingId
                                                                      && x.userId == tracking.userId);

                    if (shipTrack != null) {

                        ctx.Entry(shipTrack).State = EntityState.Deleted;
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