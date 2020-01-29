using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class Users {

        public static string KEY = "jVXVDnRLYJu+rjwiRQv5zp8fat8VE7tao1shztSVuhE=";


        public List<users> getUsers() {
            var list = new List<users>();
            users p;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var temporal = ctx.users.ToList();
                    foreach (var q in temporal) {
                        p = new users();
                        p.userId = q.userId;
                        p.customerId = q.customerId;
                        p.full_name = q.full_name;
                        p.email = q.email;
                        p.password = TransShip.Security.Security.Decrypt(q.password, new RijndaelManaged(), KEY);
                        p.user_type = q.user_type;
                        p.status = q.status;
                        p.role = q.role;
                        p.confirmationKey = q.confirmationKey;

                        list.Add(p);
                    }
                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //get user by id
        public users getUserByUserId(int userId) {
            var user = new users();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    user = ctx.users.Where(x => x.userId == userId).SingleOrDefault();
                    if (user != null) { 
                        user.password = TransShip.Security.Security.Decrypt(user.password, new RijndaelManaged(), KEY);
                    }
                    return user;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //function to get users for user by customer ID
        public List<users> getUserId(int id) {
            var list = new List<users>();
            users p;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var temporal = ctx.users.Where(x => x.customerId == id).ToList();
                    foreach (var q in temporal) {
                        p = new users();
                        p.userId = q.userId;
                        p.customerId = q.customerId;
                        p.full_name = q.full_name;
                        p.email = q.email;
                        p.password = TransShip.Security.Security.Decrypt(q.password, new RijndaelManaged(), KEY);
                        p.user_type = q.user_type;
                        p.status = q.status;
                        p.role = q.role;
                        p.confirmationKey = q.confirmationKey;

                        list.Add(p);
                    }
                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public users SaveUser(users model) {

            var user = new users();
            try {
                using (var ctx = new transshipEntities()) {
                    model.password = TransShip.Security.Security.Encrypt(model.password, new RijndaelManaged(), KEY);
                    user = ctx.users.Add(model);
                    ctx.SaveChanges();

                    return user;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public users UpdateUser(users model) {
            users user = new users();
            model.password = TransShip.Security.Security.Encrypt(model.password, new RijndaelManaged(), KEY);
            user = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(user).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return user;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteUser(int id) {

            string rpta;
            users user = new users();
            try {
                using (var ctx = new transshipEntities()) {
                    user = ctx.users.FirstOrDefault(x => x.userId == id);

                    if (user != null) {

                        ctx.Entry(user).State = EntityState.Deleted;
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

        public Payments getUser(string email) {
            var user = new Payments();
            Payments p;
            Memberships_levels m = new Memberships_levels();
            try {
                using (var ctx = new transshipEntities()) {
                    //ctx.Configuration.LazyLoadingEnabled = false;
                    //list = ctx.Memberships_levels.Include("customers").ToList();

                    var query = (from us in ctx.users
                                 join cs in ctx.customers on us.customerId equals cs.customerId
                                 join os in ctx.countries on cs.countryId equals os.CountryID
                                 where us.email == email
                                 select new {
                                     status = cs.status,
                                     company_name = cs.company_name,
                                     customerId = cs.customerId,
                                     address1 = cs.address1,
                                     address2 = cs.address2,
                                     state = cs.state,
                                     city = cs.city,
                                     Country = os.CountryName,
                                     countryId = os.CountryID,
                                     expired = cs.expiration_date,
                                     contactName = us.full_name,
                                     zipcod = cs.zipcode,
                                     userId = us.userId,
                                     role = us.role,
                                     UserType = us.user_type,
                                     email = us.email,
                                     password = us.password,
                                     logo = cs.logo
                                 });
                    foreach (var q in query) {
                        p = new Payments();
                        p.country = q.Country;
                        p.countryId = q.countryId;
                        p.company_name = q.company_name;
                        p.address1 = q.address1;
                        p.address2 = q.address2;
                        p.city = q.city;
                        if (q.expired != null) {
                            p.expired = q.expired;
                        } else {
                            p.expired = null;
                        }
                        p.contactName = q.contactName;
                        p.zipcode = q.zipcod;
                        p.customerId = q.customerId;
                        p.role = q.role;
                        p.userType = q.UserType;
                        p.userId = q.userId;
                        p.Email = q.email;
                        p.password = TransShip.Security.Security.Decrypt(q.password, new RijndaelManaged(), KEY);
                        p.status = q.status;
                        p.state = q.state;
                        p.logo = q.logo;
                        user = p;
                    }

                    if (user.customerId == 0) {
                       var query2 = (from us in ctx.users
                                 where us.email == email
                                 select new {
                                     userId = us.userId,
                                     role = us.role,
                                     UserType = us.user_type,
                                     email = us.email,
                                     password = us.password
                                 });
                        foreach (var q in query2) {
                            p = new Payments();
                            p.role = q.role;
                            p.userType = q.UserType;
                            p.userId = q.userId;
                            p.Email = q.email;
                            p.password = TransShip.Security.Security.Decrypt(q.password, new RijndaelManaged(), KEY);
                            user = p;
                        }
                    }

                    return user;
                }

            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }


        //fuction to update profile in customer table
        public customers UpdateProfile(customers model) {
            customers customer = new customers();

            customer = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(customer).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return customer;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string ChangePassword(int id, string email, string password, string newPass) {
            string rpta;
            Users user = new Users();
            Payments us;
            try {
                us = user.getUser(email);
                if (us.password == password) {
                    newPass = TransShip.Security.Security.Encrypt(newPass, new RijndaelManaged(), KEY);
                    users u = new users() { userId = id, email = email, password = newPass };
                    using (var ctx = new transshipEntities()) {
                        ctx.users.Attach(u);
                        ctx.Entry(u).Property(x => x.password).IsModified = true;
                        ctx.Configuration.ValidateOnSaveEnabled = false;
                        ctx.SaveChanges();
                    }
                    rpta = "Password was changed sucessfull";
                } else {
                    rpta = "Password incorrect";
                }

            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }

            return rpta;
        }

    }
}