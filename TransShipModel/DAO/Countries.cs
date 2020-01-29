using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Countries {


        public List<countries> getContries() {
            var list = new List<countries>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.countries.ToList();

                    return list;
                }
            } catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public countries SaveCountries(countries model) {

            var contrie = new countries();
            try {
                using (var ctx = new transshipEntities()) {
                    contrie = ctx.countries.Add(model);
                    ctx.SaveChanges();

                    return contrie;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public countries UpdateCountries(countries model) {
            countries countrie = new countries();

            countrie = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(countrie).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return countrie;
                }
            }catch(Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteCountries(int id) {

            string rpta;
            countries countrie = new countries();
            try {
                using (var ctx = new transshipEntities()) {
                    countrie = ctx.countries.FirstOrDefault(x => x.CountryID == id);

                    if(countrie != null) 
                    {

                        ctx.Entry(countrie).State = EntityState.Deleted;
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