using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class Recurring_Addresses {


        //public List<recurring_addresses> getAddresses(int idCustomer) {
        //    var list = new List<recurring_addresses>();
        //    try {
        //        using (var ctx = new transshipEntities()) {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            list = ctx.recurring_addresses.Where(x=>x.customerId == idCustomer).ToList();

        //            return list;
        //        }
        //    } catch(Exception e) {
        //        throw e;
        //    }
        //}

        public List<RecurringAddresses> getADresses(int idCustomer) {

            var list = new List<RecurringAddresses>();
            RecurringAddresses address;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from a in ctx.recurring_addresses
                                 join c in ctx.countries on a.countryId equals c.CountryID
                                 where a.customerId == idCustomer
                                 select new {
                                     customerId = a.customerId,
                                     addressId = a.addressId,
                                     countryId = a.countryId,
                                     country_name = c.CountryName,
                                     consignee_name = a.consignee_name,
                                     target_company = a.target_company,
                                     phone_number = a.phone_number,
                                     state = a.state,
                                     city = a.city,
                                     addres1 = a.address1,
                                     addres2 = a.address2,
                                     zipcode = a.zipcode,
                                 }).ToList();

                    foreach (var p in query) {
                        address = new RecurringAddresses();
                        address.customerId = p.customerId;
                        address.addressId =  p.addressId;
                        address.countryId = p.countryId;
                        address.Country_Name = p.country_name;
                        address.consignee_name = p.country_name;
                        address.consignee_name = p.consignee_name;
                        address.target_company = p.target_company;
                        address.phone_number = p.phone_number;
                        address.state = p.state;
                        address.city = p.city;
                        address.address1 = p.addres1;
                        address.address2 = p.addres2;
                        address.zipcode = p.zipcode;
                        list.Add(address);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public recurring_addresses getOneAddress(int idAddress) {
            var address = new recurring_addresses();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    address = ctx.recurring_addresses.Where(x => x.addressId == idAddress).SingleOrDefault();
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
            return address;
        }

        public recurring_addresses SaveAddress(recurring_addresses model) {

            var address = new recurring_addresses();
            try {
                using (var ctx = new transshipEntities()) {
                    address = ctx.recurring_addresses.Add(model);
                    ctx.SaveChanges();

                    return address;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public recurring_addresses UpdateAddress(recurring_addresses model) {
            recurring_addresses address = new recurring_addresses();

            address = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(address).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return address;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteAddres(int idAddress) {

            string rpta;
            recurring_addresses address = new recurring_addresses();
            try {
                using (var ctx = new transshipEntities()) {
                    address = ctx.recurring_addresses.FirstOrDefault(x => x.addressId == idAddress);

                    if(address != null) 
                    {

                        ctx.Entry(address).State = EntityState.Deleted;
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