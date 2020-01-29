using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Service_types {

        public List<service_types> getServices() {
            var list = new List<service_types>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.service_types.ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
        
        public service_types SaveService(service_types model) {

            var service = new service_types();
            try {
                using (var ctx = new transshipEntities()) {
                    service = ctx.service_types.Add(model);
                    ctx.SaveChanges();

                    return service;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public service_types UpdateService(service_types model) {
            service_types service = new service_types();

            service = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(service).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return service;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteService(int id) {

            string rpta;
            service_types service = new service_types();
            try {
                using (var ctx = new transshipEntities()) {
                    service = ctx.service_types.FirstOrDefault(x => x.service_typeId == id);

                    if (service != null) {

                        ctx.Entry(service).State = EntityState.Deleted;
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