using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOCustomers {

        Customers customer = new Customers();

        public List<customers> GetCustomers() {
            return customer.getCustomers();
        }

        public customers GetCustomerByCustomerId(int customerId) {
            return customer.getCustomerByCustomerId(customerId);
        }

        public customers SaveCustomer(customers model) {
            return customer.SaveCustomer(model);
        }

        public customers UpdateCustomer(customers model) {
            return customer.UpdateCustomer(model);
        }

        public string DeleteCustomer(int id) {
            return customer.DeleteCustomer(id);
        }
    }
}
