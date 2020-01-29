using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TransShip.LogBook;
using TransShipModel.DTO;

namespace TransShipModel.DAO {
    public class ShipmentNotes {

        //get shipment_Notes by shimpentId
        public List<Notes> getShipNotes(int shipmentId) {

            var list = new List<Notes>();
            Notes notes;
            try {
                using (var ctx = new transshipEntities()) {
                    var query = (from sn in ctx.shipment_notes
                                 join u in ctx.users on sn.userId equals u.userId
                                 where sn.shipmentId == shipmentId
                                 orderby sn.creation_date descending
                                 select new {
                                     username = u.email,
                                     userId = u.userId,
                                     shipment_noteId = sn.shipment_noteId,
                                     note = sn.note,
                                     status = sn.status,
                                     creation_date = sn.creation_date,
                                     shipmentId = sn.shipmentId
                                 }).ToList();

                    foreach (var n in query) {
                        notes = new Notes();
                        notes.username = n.username;
                        notes.userId = n.userId;
                        notes.shipment_noteId = n.shipment_noteId;
                        notes.note = n.note;
                        notes.status = n.status;
                        notes.creation_date = n.creation_date;
                        notes.shipmentId = n.shipmentId;
                        list.Add(notes);
                    }

                    return list;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        //save a new shipment_note
        public shipment_notes saveShipmentNote(shipment_notes model) {

            var shipNote = new shipment_notes();
            try {
                using (var ctx = new transshipEntities()) {
                    shipNote = ctx.shipment_notes.Add(model);
                    ctx.SaveChanges();

                    return shipNote;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

        public shipment_notes updateNote(shipment_notes model) {
            shipment_notes note = new shipment_notes();

            note = model;
            try {
                using (var ctx = new transshipEntities()) {
                    ctx.Entry(note).State = EntityState.Modified;

                    ctx.SaveChanges();

                    return note;
                }
            } catch (Exception e) {
                LogBook.TextLog.Info(string.Format("{0} {1}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty));
                throw e;
            }
        }

    }
}