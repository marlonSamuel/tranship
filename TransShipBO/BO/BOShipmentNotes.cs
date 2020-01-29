using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
   public class BOShipmentNotes {
        ShipmentNotes shipNote = new ShipmentNotes();

        public List<Notes> getShipNotes(int shipmentId) {
            return shipNote.getShipNotes(shipmentId);
        }

        public shipment_notes saveShipmentNote(shipment_notes model) {
            model.creation_date = DateTime.Today;
            model.status = "U";

            return shipNote.saveShipmentNote(model);
        }

        public shipment_notes updateNote(shipment_notes model) {
            return shipNote.updateNote(model);
        }
    }
}
