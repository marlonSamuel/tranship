using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class MemberShip_Level {

        public List<Memberships_levels> getMemberShip() {
            var list = new List<Memberships_levels>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Memberships_levels.ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public Memberships_levels SaveMemberShip(Memberships_levels model) {

            var member = new Memberships_levels();
            try {
                using (var ctx = new transshipEntities()) {
                    member = ctx.Memberships_levels.Add(model);
                    ctx.SaveChanges();

                    return member;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public Memberships_levels UpdateMember(Memberships_levels model) {
            Memberships_levels member = new Memberships_levels();

            member = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(member).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return member;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteMember(int id) {

            string rpta;
            Memberships_levels member = new Memberships_levels();
            try {
                using (var ctx = new transshipEntities()) {
                    member = ctx.Memberships_levels.FirstOrDefault(x => x.membershipId == id);

                    if (member != null) {

                        ctx.Entry(member).State = EntityState.Deleted;
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