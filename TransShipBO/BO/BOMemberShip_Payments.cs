using System.Collections.Generic;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;


namespace TransShipBO.BO {
    public class BOMemberShip_Payments {
        MemberShip_Payments customer = new MemberShip_Payments();

        public List<Customers_view> GetCustomers() {
            return customer.getCustomers();
        }

        public List<History> getHistory(int idCustomer) {
            return customer.getHistoy(idCustomer);
        }

        public membership_history saveHistory(membership_history history) {
            return customer.SaveHistory(history);
        }

        public membership_history updtaHistory(membership_history history) {
            return customer.UpdateHistory(history);
        }

        public string DeleteFile(int id) {
            return customer.Delete(id);
        }
    }
}
