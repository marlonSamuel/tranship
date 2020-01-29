using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class MemberShip_Payments {
       
        public List<Customers_view> getCustomers() {

            var list = new List<Customers_view>();
            try {
                using (var ctx = new transshipEntities()) {
                    list = ctx.Customers_view.Where(x=>x.customer_status == "A").ToList();
                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //get all history, filter for idcustomer and idmemberShip
        public List<History> getHistoy(int idCustomer) {

            var list = new List<History>();
            History h;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from s in ctx.Memberships_levels
                                 join cs in ctx.membership_history on s.membershipId equals cs.membershipId
                                 where cs.customerId == idCustomer
                                 select new {
                                     membership_HistoryId = cs.membership_HistoryId,
                                     description = s.description,
                                     payment_date = cs.payment_date,
                                     initial_date = cs.initial_date,
                                     final_date = cs.final_date,
                                     memeberShipId = cs.membershipId,
                                     custormerId = cs.customerId,
                                 }).ToList();

                      foreach(var p in query) {
                        h = new History();
                        h.membership_HistoryId = p.membership_HistoryId;
                        h.customerId = p.custormerId;
                        h.description = p.description;
                        h.final_date = p.final_date;
                        h.membershipId = p.memeberShipId;
                        h.initial_date = p.initial_date;
                        h.payment_date = p.payment_date;
                        list.Add(h);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
        
        public membership_history SaveHistory(membership_history model) {

            var history = new membership_history();
            try {
                using (var ctx = new transshipEntities()) {
                    history = ctx.membership_history.Add(model);
                    ctx.SaveChanges();

                    return history;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public membership_history UpdateHistory(membership_history model) {
            membership_history history = new membership_history();

            history = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(history).State = EntityState.Modified;
                    ctx.SaveChanges();

                    return history;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string Delete(int id) {

            string rpta;
            membership_history history = new membership_history();
            try {
                using (var ctx = new transshipEntities()) {
                    history = ctx.membership_history.FirstOrDefault(x => x.membership_HistoryId == id);

                    if (history != null) {

                        ctx.Entry(history).State = EntityState.Deleted;
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