using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using TransShip.LogBook;

namespace TransShipModel.DAO {
    public class Type_files {


        public List<file_types> getFiles() {
            var list = new List<file_types>();
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.file_types.ToList();

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public file_types SaveFiles(file_types model) {

            var file = new file_types();
            try {
                using (var ctx = new transshipEntities()) {
                    file = ctx.file_types.Add(model);
                    ctx.SaveChanges();

                    return file;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public file_types UpdateFiles(file_types model) {
            file_types file = new file_types();

            file = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(file).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return file;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public string DeleteFile(int id) {

            string rpta;
            file_types file = new file_types();
            try {
                using (var ctx = new transshipEntities()) {
                    file = ctx.file_types.FirstOrDefault(x => x.file_typeId == id);

                    if (file != null) {

                        ctx.Entry(file).State = EntityState.Deleted;
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