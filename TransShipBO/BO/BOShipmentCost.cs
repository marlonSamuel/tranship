using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOShipmentCost {
        ShipmentCost shipCost = new ShipmentCost();

        public List<shipment_cost> Get(int shipmentId) {
            return shipCost.get(shipmentId);
        }

        public shipment_cost Save(shipment_cost model) {
            return shipCost.Save(model);
        }

        public shipment_cost update(shipment_cost model) {
            return shipCost.update(model);
        }

        public string Delete(shipment_cost model) {
            return shipCost.Delete(model);
        }
    }
}
