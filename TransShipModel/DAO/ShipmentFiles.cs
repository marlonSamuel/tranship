using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class ShipmentFiles {

        //get shipment_Files by shimpentId
        public List<Files> getShipmentFiles(int shipmentId) {

            var list = new List<Files>();
            Files files;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from sa in ctx.shipment_attachment
                                 join u in ctx.users on sa.userId equals u.userId
                                 join t in ctx.file_types on sa.file_typeId equals t.file_typeId
                                 where sa.shipmentId == shipmentId
                                 orderby sa.creation_date descending
                                 select new {
                                     username = u.email,
                                     userId = u.userId,
                                     file_typeId = t.file_typeId,
                                     type = t.type,
                                     shipment_attachmentId = sa.shipment_attachmentId,
                                     creation_date = sa.creation_date,
                                     file_name = sa.file_name,
                                     description = sa.description,
                                     shipmentId = sa.shipmentId,
                                     customerId = u.customerId,
                                 }).ToList();

                    foreach (var f in query) {
                        files = new Files();
                        files.username = f.username;
                        files.userId = f.userId;
                        files.shipment_attachmentId = f.shipment_attachmentId;
                        files.type = f.type;
                        files.creation_date = f.creation_date;
                        files.description = f.description;
                        files.file_name = f.file_name;
                        files.shipmentId = f.shipmentId;
                        files.customerId = f.customerId;
                        list.Add(files);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //save a new shipment_note
        public shipment_attachment saveShipentFile(shipment_attachment model) {

            var file = new shipment_attachment();
            try {
                using (var ctx = new transshipEntities()) {
                    file = ctx.shipment_attachment.Add(model);
                    ctx.SaveChanges();

                    return file;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }
    }
}