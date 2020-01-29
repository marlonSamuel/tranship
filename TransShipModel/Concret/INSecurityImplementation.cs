using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TransShipModel.Abstract;
using TransShip.LogBook;
using System.Security.Cryptography;
using TransShipModel.DTO;
using NLog;
using NLog.Targets;

namespace TransShipModel.Concret {
    public class INSecurityImplementation : INSecurity
    {
        public static string KEY = DAO.Users.KEY;

        private readonly transshipEntities _transshipEntities = new transshipEntities();

        public PendingApprobalObject GetPendingApprobalRecord(decimal id)
        {
            var returnValue = new PendingApprobalObject();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from c in _transshipEntities.customers
                                join u in _transshipEntities.users on c.customerId equals u.customerId
                                join cc in _transshipEntities.customer_contacts on c.customerId equals cc.customerId
                                where c.customerId == id
                                select new PendingApprobalObject
                                {
                                    customerId = u.customerId,
                                    userId = u.userId,
                                    username = u.email,
                                    companyname = c.company_name,
                                    fullname = u.full_name,
                                    title = cc.title,
                                    phonenumber = cc.phone_number,
                                    email = u.email,
                                    addr1 = c.address1,
                                    addr2 = c.address2,
                                    zipcode = c.zipcode,
                                    idpais = c.countryId,
                                    outsideus = c.state,
                                    usstate = c.state,
                                    canadaprovince = c.state,
                                    membership = c.estimated_membership,
                                    city = c.city
                                }).ToList();
                returnValue = temporal.Any() ? temporal.First() : new PendingApprobalObject();
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public decimal UpdateUserState (decimal? customerId, decimal? userId, string newState, string baseURI)
        {
            var returnValue = (long)0;
            var stopwatch = new Stopwatch();
            string email="";
            string passwd = "";
            stopwatch.Start();
            try
            {
                var userEntity = _transshipEntities.users.First(x => x.userId == userId);
                userEntity.status = newState;
                email = userEntity.email;
                passwd = userEntity.password;
                _transshipEntities.SaveChanges();

                var customerEntity = _transshipEntities.customers.First(x=> x.customerId == customerId);
                if (newState == "A") {
                    customerEntity.status = "A";
                    _transshipEntities.SaveChanges();
                }else {
                    customerEntity.status = "R";
                    _transshipEntities.SaveChanges();
                }

                returnValue = 1000;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                var emailhandler = new TransShip.EMailHandler.EMailHandler();
                try {
                    if (newState == "A") 
                    {
                        emailhandler.SendConfirmationEmail(string.Format("Your request has been approved, please enter the following link"), 
                            string.Format(baseURI + "/home"), email);
                    } else {
                        emailhandler.SendConfirmationEmail(string.Format("Your request has been rejected, please contact TransShip"), 
                           string.Format(""), email);
                    }
                    stopwatch.Stop();
                } catch(Exception e) {
                    throw e;
                }
                
            }
            return returnValue;
        }

        public List<CusotmerInformation> GetPendingCustomers()
        {
            var returnValue = new List<CusotmerInformation>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                returnValue = (from c in _transshipEntities.customers join d in _transshipEntities.users
                               on c.customerId equals d.customerId
                               join cc in _transshipEntities.customer_contacts 
                               on c.customerId equals cc.customerId
                               where c.status.Equals("P", StringComparison.InvariantCultureIgnoreCase)
                               select new CusotmerInformation {
                                   CustomerId = c.customerId,
                                   CompanyName = c.company_name,
                                   contact_name = d.full_name,
                                   register_date = c.register_date,
                                   phone_number = cc.phone_number,
                                   UserId = d.userId
                               }).ToList();

            } catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public decimal EraseCountry(CountryInformation userInformation)
        {
            var returnValue = (decimal)0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userEntity = _transshipEntities.countries.First(x => x.CountryID == userInformation.Id);
                _transshipEntities.countries.Remove(userEntity);
                _transshipEntities.SaveChanges();
                returnValue = 1000;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<CountryInformation> GetCountryInformation(long id)
        {
            var returnValue = new List<CountryInformation>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from u in _transshipEntities.countries
                                where u.CountryID == id
                                select new CountryInformation
                                {
                                    ISO2 = u.ISO2,
                                    Name = u.CountryName,
                                    LongCountryName = u.LongCountryName,
                                    ISO3 = u.ISO3,
                                    NumCode = u.NumCode,
                                    UNMemberState = u.UNMemberState,
                                    CallingCode = u.CallingCode,
                                    CCTLD = u.CCTLD,
                                    Id = u.CountryID
                                }).ToList();
                returnValue = temporal;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public long UpdateCountry(CountryInformation countryInformation)
        {
            var returnValue = (long)0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userEntity = _transshipEntities.countries.First(x => x.CountryID == countryInformation.Id);
                userEntity.ISO2 = countryInformation.ISO2;
                userEntity.CountryName = countryInformation.Name;
                userEntity.LongCountryName = countryInformation.LongCountryName;
                userEntity.ISO3 = countryInformation.ISO3;
                userEntity.NumCode = countryInformation.NumCode;
                userEntity.UNMemberState = countryInformation.UNMemberState;
                userEntity.CallingCode = countryInformation.CallingCode;
                userEntity.CCTLD = countryInformation.CCTLD;
                _transshipEntities.SaveChanges();
                returnValue = userEntity.CountryID;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public long AddNewCountry(CountryInformation countryInformation)
        {
            var returnValue = (long)0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var newCountry = new countries
                {
                    ISO2 = countryInformation.ISO2,
                    CountryName = countryInformation.Name,
                    LongCountryName = countryInformation.LongCountryName,
                    ISO3 = countryInformation.ISO3,
                    NumCode = countryInformation.NumCode,
                    UNMemberState = countryInformation.UNMemberState,
                    CallingCode = countryInformation.CallingCode,
                    CCTLD = countryInformation.CCTLD
                };
                _transshipEntities.countries.Add(newCountry);
                _transshipEntities.SaveChanges();
                returnValue = newCountry.CountryID;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));                
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<CountryInformation> GetCountryList()
        {
            var returnValue = new List<CountryInformation>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                returnValue = (from u in _transshipEntities.countries
                               select new CountryInformation
                               {
                                   Id = u.CountryID,
                                   Name = u.CountryName,
                                   LongCountryName = u.LongCountryName,
                                   ISO3 = u.ISO3,
                                   NumCode = u.NumCode,
                                   UNMemberState = u.UNMemberState,
                                   CallingCode = u.CallingCode,
                                   CCTLD = u.CCTLD,
                                   ISO2 = u.ISO2
                               }).ToList();
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public decimal EraseUser(UserInformation userInformation)
        {
            var returnValue = (decimal)0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userEntity = _transshipEntities.users.First(x => x.userId == userInformation.id);                
                _transshipEntities.users.Remove(userEntity);
                _transshipEntities.SaveChanges();
                returnValue = 1000;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public decimal ModifyUser(UserInformation userInformation)
        {
            var returnValue = (decimal)0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userEntity = _transshipEntities.users.First(x => x.userId == userInformation.id);
                userEntity.full_name = userInformation.FullName;
                userEntity.email = userInformation.EMail;
                userEntity.status = userInformation.Status;
                userEntity.role = userInformation.Role;
                _transshipEntities.SaveChanges();
                returnValue = userEntity.userId;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<UserInformation> GetUserInformation(decimal userId)
        {
            var returnValue = new List<UserInformation>();
            UserInformation h;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from u in _transshipEntities.users
                                where u.userId == userId
                                select new UserInformation
                                {
                                    id = u.userId,
                                    customerId = u.customerId,
                                    FullName = u.full_name,
                                    EMail = u.email,
                                    Password = u.password,
                                    Role = u.role,
                                    Status = u.status,
                                    user_type = u.user_type
                                }).ToList();
                foreach (var u in temporal) {
                    h = new UserInformation();
                    h.id = u.id;
                    h.customerId = u.customerId;
                    h.FullName = u.FullName;
                    h.EMail = u.EMail;
                    h.Password = TransShip.Security.Security.Decrypt(u.Password, new RijndaelManaged(), KEY);
                    h.Role = u.Role;
                    h.Status = u.Status;
                    h.user_type = u.user_type;
                    returnValue.Add(h);
                }
                
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<UserInformation> GetUserInformation(string email) {
            var returnValue = new List<UserInformation>();
            UserInformation h;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try {
                var temporal = (from u in _transshipEntities.users
                                where u.email == email
                                select new UserInformation {
                                    id = u.userId,
                                    customerId = u.customerId,
                                    FullName = u.full_name,
                                    EMail = u.email,
                                    Password = u.password,
                                    Role = u.role,
                                    Status = u.status,
                                    user_type = u.user_type
                                }).ToList();
                foreach (var u in temporal) {
                    h = new UserInformation();
                    h.id = u.id;
                    h.customerId = u.customerId;
                    h.FullName = u.FullName;
                    h.EMail = u.EMail;
                    h.Password = TransShip.Security.Security.Decrypt(u.Password, new RijndaelManaged(), KEY);
                    h.Role = u.Role;
                    h.Status = u.Status;
                    h.user_type = u.user_type;
                    returnValue.Add(h);
                }
               
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            } finally {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<UserInformation> GetUserList()
        {
            var returnValue = new List<UserInformation>();
            UserInformation h;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from u in _transshipEntities.users
                                select new UserInformation
                                {
                                    id = u.userId,
                                    customerId = u.customerId,
                                    FullName = u.full_name,
                                    EMail = u.email,
                                    Password = u.password,
                                    Role = u.role,
                                    Status = u.status,
                                    user_type = u.user_type
                                }).ToList();
                foreach (var u in temporal) {
                    h = new UserInformation();
                    h.id = u.id;
                    h.customerId = u.customerId;
                    h.FullName = u.FullName;
                    h.EMail = u.EMail;
                    h.Password = TransShip.Security.Security.Decrypt(u.Password, new RijndaelManaged(), KEY);
                    h.Role = u.Role;
                    h.Status = u.Status;
                    returnValue.Add(h);
                }
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public string ChangePassword(ChangePassword information, string baseURI)
        {
            var returnValue = string.Empty;            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userToConfirm = _transshipEntities.users.Where(x => x.confirmationKey == information.APIKey).First();
                //userToConfirm.status = "F";
                userToConfirm.password = TransShip.Security.Security.Encrypt(information.password, new RijndaelManaged(), KEY);
                _transshipEntities.SaveChanges();
                returnValue = "done";
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {                
                stopwatch.Stop();
            }
            return returnValue;
        }

        public string ConfirmUserName(string keyInformation)
        {            
            LogBook.TextLog.Info("{0}",keyInformation);
            var returnValue = string.Empty;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userToConfirm = _transshipEntities.users.First(x => x.confirmationKey.Equals(keyInformation, 
                    StringComparison.Ordinal));
                userToConfirm.status = "C"; //Confirmed
                _transshipEntities.SaveChanges();
                returnValue = "done";
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public string RecoverPassword(string email, string baseURI)
        {
            var returnValue = string.Empty;
            var passwd = TransShip.Security.Security.Encrypt(DateTime.Now.ToString(), new RijndaelManaged(), TransShip.Security.Security.GenerateKey(new RijndaelManaged(), 256));
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var userToConfirm = _transshipEntities.users.First(x => x.email.Equals(email,
                    StringComparison.Ordinal));
                //userToConfirm.status = "R"; //Recover Password
                userToConfirm.confirmationKey = passwd;
                _transshipEntities.SaveChanges();
                returnValue = "done";                
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {3}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                if (string.IsNullOrEmpty(returnValue) == false)
                {
                    var emailhandler = new TransShip.EMailHandler.EMailHandler();
                    emailhandler.SendConfirmationEmail(string.Format("your password has been recovered, please enter the following link"), string.Format("{0}/Home/RecoverPassword?apiInformation={1}", baseURI,
                        HttpUtility.UrlEncode(passwd)), email);
                }
                stopwatch.Stop();
            }
            return returnValue;
        }

        public decimal CreateUser(CreateUserObject createUserObject, string baseURI)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var returnValue = (decimal)0;
            var passwd = string.Empty;
            using (var dbTran = _transshipEntities.Database.BeginTransaction())
            {
                try
                {
                    var newCustomer = new customers {
                        company_name = createUserObject.companyname,
                        countryId = createUserObject.idCountry,
                        city = createUserObject.city,
                        estimated_membership = createUserObject.membership,
                        address1 = createUserObject.addr1,
                        address2 = createUserObject.addr2,
                        zipcode = createUserObject.zipcode,
                        status = "P",
                        register_date = DateTime.Today,
                        state = createUserObject.usstate

                    };

                    _transshipEntities.customers.Add(newCustomer);
                    
                    passwd = TransShip.Security.Security.Encrypt(DateTime.Now.ToString(), new RijndaelManaged(), TransShip.Security.Security.GenerateKey(new RijndaelManaged(), 256));
                    var newUser = new users
                    {
                        customerId = newCustomer.customerId,
                        full_name = createUserObject.fullname,
                        email = createUserObject.email,
                        password = TransShip.Security.Security.Encrypt(createUserObject.keyinformation, new RijndaelManaged(), KEY),
                        user_type = "C",
                        status = "I",
                        role = "A",
                        confirmationKey = passwd
                    };
                    _transshipEntities.users.Add(newUser);

                    var newContact = new customer_contacts {
                        customerId = newCustomer.customerId,
                        full_name = createUserObject.fullname,
                        title = createUserObject.title,
                        phone_number = createUserObject.phonenumber,
                        email = createUserObject.email
                    };

                    _transshipEntities.customer_contacts.Add(newContact);
                    _transshipEntities.SaveChanges();
                    returnValue = newUser.userId;
                    dbTran.Commit();
                }
                catch (Exception e)
                {
                    LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                }
                finally
                {
                    stopwatch.Stop();
                    LogBook.TextLog.Info(string.Format("Ellapsed time {0} milli seconds", stopwatch.ElapsedMilliseconds));
                    var emailhandler = new TransShip.EMailHandler.EMailHandler();

                    emailhandler.SendConfirmationEmail(string.Format("your registration has been successful, TranShip will contact you "), 
                        string.Format("{0}?apiInformation={1}", baseURI, HttpUtility.UrlEncode(passwd)), createUserObject.email);
                }
            }
            return returnValue;
        }

        public List<MembershipLelvelObject> GetMembershipLevelList()
        {
            var returnValue = new List<MembershipLelvelObject>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from u in _transshipEntities.Memberships_levels
                                select new MembershipLelvelObject
                                {
                                    Id = u.membershipId,
                                    Description = u.description
                                }).ToList();
                returnValue = temporal;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public List<Country> GetAllCountries()
        {
            var returnValue = new List<Country>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var temporal = (from u in _transshipEntities.countries
                                select new Country
                                {
                                    Id = u.CountryID,
                                    Name = u.CountryName
                                }).ToList();
                returnValue = temporal;
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }

        public bool CheckUserName(string userName, string passWord)
        {
            var returnValue = false;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var dPasswd = TransShip.Security.Security.Encrypt(passWord, new RijndaelManaged(), KEY);
                var temporal = from u in _transshipEntities.users
                               where
                               u.email.Equals(userName, StringComparison.InvariantCultureIgnoreCase)
                               && u.password.Equals(dPasswd, StringComparison.InvariantCultureIgnoreCase) 
                               && u.status == "A"
                               select new
                               {
                                   xyz = u.userId,
                                   status = u.status
                               };

                
                    returnValue = temporal.Any();
            }
            catch (Exception e)
            {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));                
            }
            finally
            {
                stopwatch.Stop();
            }
            return returnValue;
        }
    }
}