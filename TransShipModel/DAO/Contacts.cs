using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Contacts {


        public List<customer_contacts> getContacts(int idCustomer) {
            var list = new List<customer_contacts>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.customer_contacts.Where(x=>x.customerId == idCustomer).ToList();

                    return list;
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public customer_contacts SaveContact(customer_contacts model) {

            var contact = new customer_contacts();
            try {
                using (var ctx = new transshipEntities()) {
                    contact = ctx.customer_contacts.Add(model);
                    ctx.SaveChanges();

                    return contact;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public customer_contacts UpdateContact(customer_contacts model) {
            customer_contacts contact = new customer_contacts();
            contact = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(contact).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return contact;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteContact(int idContact) {

            string rpta;
            customer_contacts contact = new customer_contacts();
            try {
                using (var ctx = new transshipEntities()) {
                    contact = ctx.customer_contacts.FirstOrDefault(x => x.contactId == idContact);

                    if(contact != null) 
                    {

                        ctx.Entry(contact).State = EntityState.Deleted;
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