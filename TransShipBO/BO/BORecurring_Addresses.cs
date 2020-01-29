using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BORecurring_Addresses {

        Recurring_Addresses address = new Recurring_Addresses();

        public List<RecurringAddresses> GetAddress(int idCustomer) {
            return address.getADresses(idCustomer);
        }

        public recurring_addresses getOneAddres(int idAddres) {
            return address.getOneAddress(idAddres);
        }
        public recurring_addresses SaveAddress(recurring_addresses model) {
            return address.SaveAddress(model);
        }

        public recurring_addresses UpdateAddress(recurring_addresses model) {
            return address.UpdateAddress(model);
        }

        public string DeleteAddress(int idAddress) {
            return address.DeleteAddres(idAddress);
        }
    }
}
