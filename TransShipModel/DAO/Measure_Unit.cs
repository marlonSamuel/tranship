using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Measure_Unit {

        public List<measure_units> get() {
            var list = new List<measure_units>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.measure_units.ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public List<measure_units> getUnitByService(int id) {
            var service = new List<measure_units>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    service = ctx.measure_units.Where(x => x.service_typeId == id).ToList();

                    return service;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public measure_units SaveUnit(measure_units model) {

            var unit = new measure_units();
            try {
                using (var ctx = new transshipEntities()) {
                    unit = ctx.measure_units.Add(model);
                    ctx.SaveChanges();

                    return unit;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public measure_units UpdateUnit(measure_units model) {
            measure_units unit = new measure_units();

            unit = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(unit).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return unit;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteUnit(int id) {

            string rpta;
            measure_units unit = new measure_units();
            try {
                using (var ctx = new transshipEntities()) {
                    unit = ctx.measure_units.FirstOrDefault(x => x.measure_unitId == id);

                    if (unit != null) {

                        ctx.Entry(unit).State = EntityState.Deleted;
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