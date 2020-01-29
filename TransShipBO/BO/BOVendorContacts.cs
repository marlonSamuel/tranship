using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOVendorContacts {

        VendorContacts vendorContacts = new VendorContacts();

        public List<vendor_contacts> GetVendorContacts() {
            return vendorContacts.getVendorContacts();
        }

        public vendor_contacts SaveVendorContacts(vendor_contacts vendor_contacts) {
            return vendorContacts.SaveVendorContacts(vendor_contacts);
        }

        public vendor_contacts UpdateVendorContacts(vendor_contacts vendor_contacts) {
            return vendorContacts.UpdateVendorContacts(vendor_contacts);
        }

        public string DeleteVendorContacts(vendor_contacts vendor_contacts) {
            return vendorContacts.DeleteVendorContacts(vendor_contacts);
        }
    }
}
