using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BOShipmentTracking {
        ShipmentTracking shipTracking = new ShipmentTracking();

        public List<Tracking> GetShipmentTracking(int shipmentId) {
            return shipTracking.getShipTracking(shipmentId);
        }

        public List<shipment_tracking> Get(int shipmentId) {
            return shipTracking.get(shipmentId);
        }

        public shipment_tracking Save(shipment_tracking model) {
            return shipTracking.Save(model);
        }

        public shipment_tracking update(shipment_tracking model) {
            return shipTracking.update(model);
        }

        public string Delete(shipment_tracking model) {
            return shipTracking.Delete(model);
        }
    }
}
