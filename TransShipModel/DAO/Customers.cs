using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Customers {


        public List<customers> getCustomers() {
            var list = new List<customers>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.customers.ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public customers getCustomerByCustomerId(int customerId) {
            customers customer = new customers();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    customer = ctx.customers.FirstOrDefault(x => x.customerId == customerId);

                    return customer;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public customers SaveCustomer(customers model) {

            var customer = new customers();
            try {
                using (var ctx = new transshipEntities()) {
                    customer = ctx.customers.Add(model);
                    ctx.SaveChanges();

                    return customer;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public customers UpdateCustomer(customers model) {
            customers customer = new customers();

            customer = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(customer).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return customer;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteCustomer(int id) {

            string rpta;
            customers customer = new customers();
            try {
                using (var ctx = new transshipEntities()) {
                    customer = ctx.customers.FirstOrDefault(x => x.customerId == id);

                    if(customer != null) 
                    {

                        ctx.Entry(customer).State = EntityState.Deleted;
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