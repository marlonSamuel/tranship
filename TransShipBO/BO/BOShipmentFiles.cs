using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BOShipmentFiles {
        ShipmentFiles file = new ShipmentFiles();

        //get shipment files by shipmentId
        public List<Files> getShipmentFiles(int shipmentId) {
            return file.getShipmentFiles(shipmentId);
        }

        public shipment_attachment saveShipmentFile(shipment_attachment model) {
            return file.saveShipentFile(model);
        }
    }
}
