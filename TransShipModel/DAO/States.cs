using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class States {

        public List<state> getStates(int id) {
            var list = new List<state>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.state.Where(x => x.countryId == id).ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public state SaveState(state model) {

            var stat = new state();
            try {
                using (var ctx = new transshipEntities()) {
                    
                    stat = ctx.state.Add(model);
                    ctx.SaveChanges();

                    return stat;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public state UpdateState(state model) {
            state stat = new state();

            stat = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(stat).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return stat;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteState(string id) {

            string rpta;
            state stat = new state();
            try {
                using (var ctx = new transshipEntities()) {
                    stat = ctx.state.FirstOrDefault(x => x.stateId == id);

                    if (stat != null) {

                        ctx.Entry(stat).State = EntityState.Deleted;
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