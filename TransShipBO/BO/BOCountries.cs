using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOCountries {

        Countries country = new Countries();

        public List<countries> GetCountries() {
            return country.getContries();
        }

        public countries SaveCountris(countries countries) {
            return country.SaveCountries(countries);
        }

        public countries UpdateCountris(countries countries) {
            return country.UpdateCountries(countries);
        }

        public string DeleteCountries(int id) {
            return country.DeleteCountries(id);
        }
    }
}
