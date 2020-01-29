using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOContacts {

        Contacts contact = new Contacts();

        public List<customer_contacts> GetContact(int idCustomer) {
            return contact.getContacts(idCustomer);
        }

        public customer_contacts SaveContact(customer_contacts model) {
            return contact.SaveContact(model);
        }

        public customer_contacts UpdateContact(customer_contacts model) {
            return contact.UpdateContact(model);
        }

        public string DeleteContact(int idContact) {
            return contact.DeleteContact(idContact);
        }
    }
}
