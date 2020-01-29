using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BOShipmentApprove {
        ShipmentApprove servicecost = new ShipmentApprove();

        public List<ShipServiceCost> getCostsServicesByShipmentId(int shipmentId) {
            return servicecost.getCostsServicesByShipmentId(shipmentId);
        }

        public List<ShipCosts> getCostsByShipmentId(int shipmentId) {
            return servicecost.getCostsByShipmentId(shipmentId);
        }
    }
}
