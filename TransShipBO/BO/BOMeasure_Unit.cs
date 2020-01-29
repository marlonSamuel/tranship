using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOMeasure_Unit {
        Measure_Unit unit = new Measure_Unit();

        public List<measure_units> Get() {
            return unit.get();
        }

        public List<measure_units> GetUnitByService(int id) {
            return unit.getUnitByService(id);
        }


        public measure_units SaveUnit(measure_units uni) {
            return unit.SaveUnit(uni);
        }

        public measure_units UpdateUnit(measure_units uni) {
            return unit.UpdateUnit(uni);
        }

        public string DeleteUnit(int id) {
            return unit.DeleteUnit(id);
        }

    }
}
