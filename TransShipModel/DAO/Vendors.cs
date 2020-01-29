using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Vendors {


        public List<vendors> getVendors() {
            var list = new List<vendors>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.vendors.ToList();

                    return list;
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public vendors getVendorById(int vendorId) {
            var vendor = new vendors();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    vendor = ctx.vendors.Where(x => x.vendorId == vendorId).SingleOrDefault();
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
            return vendor;
        }

        public vendors SaveVendors(vendors model) {

            var vendor = new vendors();
            try {
                using (var ctx = new transshipEntities()) {
                    vendor = ctx.vendors.Add(model);
                    ctx.SaveChanges();

                    return vendor;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public vendors UpdateVendors(vendors model) {
            vendors vendor = new vendors();

            vendor = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(vendor).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return vendor;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteVendors(int id) {

            string rpta;
            vendors vendor = new vendors();
            try {
                using (var ctx = new transshipEntities()) {
                    vendor = ctx.vendors.FirstOrDefault(x => x.vendorId == id);

                    if(vendor != null) 
                    {

                        ctx.Entry(vendor).State = EntityState.Deleted;
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