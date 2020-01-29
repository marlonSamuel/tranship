using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BORates {

        Rates rate = new Rates();

        public List<rates> GetRates() {
            return rate.getRates();
        }

        public List<RatesObject> GetRatesVendor(int vendorId) {
            return rate.getRatesVendor(vendorId);
        }

        public rates SaveRates(rates rates) {
            return rate.SaveRates(rates);
        }

        public rates UpdateRates(rates rates) {
            return rate.UpdateRates(rates);
        }

        public string DeleteRates(rates rates) {
            return rate.DeleteRates(rates);
        }
    }
}
