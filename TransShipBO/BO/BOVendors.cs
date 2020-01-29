using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOVendors {

        Vendors vendor = new Vendors();

        public List<vendors> GetVendors() {
            return vendor.getVendors();
        }

        public vendors getVendorById(int vendorId) {
            return vendor.getVendorById(vendorId);
        }

        public vendors SaveVendors(vendors vendors) {
            return vendor.SaveVendors(vendors);
        }

        public vendors UpdateVendors(vendors vendors) {
            return vendor.UpdateVendors(vendors);
        }

        public string DeleteVendors(int id) {
            return vendor.DeleteVendors(id);
        }
    }
}
