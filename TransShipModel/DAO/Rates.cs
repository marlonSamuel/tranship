using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Rates {


        public List<rates> getRates() {
            var list = new List<rates>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.rates.ToList();

                    return list;
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //get all rates by vendor
        public List<RatesObject> getRatesVendor(int vendorId) {

            var list = new List<RatesObject>();
            RatesObject h;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from s in ctx.rates
                                 join cs in ctx.countries on s.countryId equals cs.CountryID
                                 join st in ctx.service_types on s.service_typeId equals st.service_typeId
                                 join mu in ctx.measure_units on s.measure_unitId equals mu.measure_unitId
                                 where s.vendorId == vendorId
                                 select new {
                                     vendorId = s.vendorId,
                                     countryId = s.countryId,
                                     countryName = cs.CountryName,
                                     service_typeId = s.service_typeId,
                                     serviceTypeDescription = st.descripcion,
                                     measure_unitId = s.measure_unitId,
                                     measureUnitDescription = mu.description,
                                     state = s.state,
                                     price = s.price,
                                     status =  s.status
                                 }).ToList();

                      foreach(var p in query) {
                        h = new RatesObject();
                        h.vendorId = p.vendorId;
                        h.countryId = p.countryId;
                        h.countryName = p.countryName;
                        h.service_typeId = p.service_typeId;
                        h.serviceTypeDescription = p.serviceTypeDescription;
                        h.measure_unitId = p.measure_unitId;
                        h.measureUnitDescription = p.measureUnitDescription;
                        h.state = p.state;
                        h.price = p.price;
                        h.status =  p.status;
                        list.Add(h);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public rates SaveRates(rates model) {

            var rate = new rates();
            try {
                using (var ctx = new transshipEntities()) {
                    rate = ctx.rates.Add(model);
                    ctx.SaveChanges();

                    return rate;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public rates UpdateRates(rates model) {
            rates rate = new rates();

            rate = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(rate).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return rate;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteRates(rates rates ) {

            string rpta;
            rates rate = new rates();
            try {
                using (var ctx = new transshipEntities()) {
                    rate = ctx.rates.FirstOrDefault(x => (x.vendorId == rates.vendorId && x.countryId == rates.countryId 
                                                    &&    x.service_typeId == rates.service_typeId && x.measure_unitId == rates.measure_unitId));

                    if(rate != null) 
                    {

                        ctx.Entry(rate).State = EntityState.Deleted;
                        ctx.SaveChanges();

                        rpta = "rate deleted";
                        
                    } else {
                        rpta = "rate not found";
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