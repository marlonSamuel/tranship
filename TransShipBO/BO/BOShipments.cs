using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BOShipments {
        Shipments shipment = new Shipments();
        ShipmentApprove approve = new ShipmentApprove();

        public List<shipments> getShipment(int idCustomer) {
            return shipment.getShipments(idCustomer);
        }

        public List<shipments> getShipmentByStatus(string status) {
            return shipment.getShipmentsByStatus(status);
        }

        public shipments getShipmentByPO(int customerId, string PO) {
            return shipment.getShipmentsByPO(customerId, PO);
        }

        public List<shipments> getShipmentByStatusAndDateRange(string status, DateTime from, DateTime to) {
            return shipment.getShipmentsByStatusAndDateRange(status, from, to);
        }

        public List<shipmentHistory> getLastTwoShipment(int customerId, string state) {
            var history = new List<shipmentHistory>();
            List<ShipServiceCost> shipServ = new List<ShipServiceCost>();
            List<ShipCosts> shipCost = new List<ShipCosts>();

            decimal totalS = 0;
            decimal? totalC = 0;
            decimal? total;
            int c = 0;

        
            shipmentHistory shipH = new shipmentHistory();

            history = shipment.getLastTwoShipment(customerId, state);

            foreach (var i in history)
            {
                shipServ = approve.getCostsServicesByShipmentId(Convert.ToInt32(i.shipmentId));

                foreach (var ss in shipServ) {
                    totalS = totalS + ss.totalServices;
                }

                shipCost = approve.getCostsByShipmentId(Convert.ToInt32(i.shipmentId));

                foreach (var sc in shipCost) {
                    totalC = totalC + sc.totalCosts;
                }

                total = totalC + totalS;
                history[c].total = Convert.ToDecimal(total);

                c++;
                totalC = 0;
                totalS = 0;
            }

            //for (int i = 0; i < shipServ.Count; i++) {
            //    history[i].total = shipServ[i].totalServices;
            //}

            return history;
        }

        public shipments getOneShipment(int shipmentId) {
            return shipment.getOneShipment(shipmentId);
        }

        public shipments saveShipment(shipments ship) {
            return shipment.saveShipment(ship);
        }

        public shipments updateShipments(shipments ship) {
            return shipment.updateShipment(ship);
        }

        public string deleteShipment(int id) {
            return shipment.deleteShipment(id);
        }

        public string checkShipmentsByMembership(int customerId) {
            return shipment.checkShipmentsByMembership(customerId);
        }
    }
}
