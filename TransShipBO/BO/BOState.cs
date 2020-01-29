using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOState {
        States stat = new States();
        public List<state> GetStates(int id) {
            return stat.getStates(id);
        }

        public state SaveState(state state) {
            return stat.SaveState(state);
        }

        public state UpdateUnit(state state) {
            return stat.UpdateState(state);
        }

        public string DeleteState(string id) {
            return stat.DeleteState(id);
        }
    }
}
