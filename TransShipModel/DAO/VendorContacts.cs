using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class VendorContacts {


        public List<vendor_contacts> getVendorContacts() {
            var list = new List<vendor_contacts>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.vendor_contacts.ToList();

                    return list;
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public vendor_contacts SaveVendorContacts(vendor_contacts model) {

            var contrie = new vendor_contacts();
            try {
                using (var ctx = new transshipEntities()) {
                    contrie = ctx.vendor_contacts.Add(model);
                    ctx.SaveChanges();

                    return contrie;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public vendor_contacts UpdateVendorContacts(vendor_contacts model) {
            vendor_contacts vendorContacts = new vendor_contacts();

            vendorContacts = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(vendorContacts).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return vendorContacts;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteVendorContacts(vendor_contacts vendorcontacts) {

            string rpta;
            vendor_contacts vendorContacts = new vendor_contacts();
            try {
                using (var ctx = new transshipEntities()) {
                    vendorContacts = ctx.vendor_contacts.FirstOrDefault(x => (x.vendorId == vendorcontacts.vendorId && x.contactId == vendorcontacts.contactId));

                    if(vendorContacts != null) 
                    {

                        ctx.Entry(vendorContacts).State = EntityState.Deleted;
                        ctx.SaveChanges();

                        rpta = "register deleted";
                        
                    } else {
                        rpta = "register not found";
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