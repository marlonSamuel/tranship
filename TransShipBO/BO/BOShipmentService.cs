using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOShipmentService {
        ShipmentServices ShipServ = new ShipmentServices();

        public List<shipment_services> GetServicesShipment(int shipmentId) {
            return ShipServ.getShipmentServices(shipmentId);
        }

        public void SetVendorPrices(int shipmentId) {
            ShipServ.setVendorPrices(shipmentId);
        }

        public shipment_services SaveShipmentService(shipment_services model) {
            return ShipServ.SaveShipmentService(model);
        }

        public shipment_services updateShipmentService(shipment_services model) {
            return ShipServ.updateShipmentService(model);
        }

        public string Delete(shipment_services shipservice) {
            return ShipServ.Delete(shipservice);
        }
    }
}
