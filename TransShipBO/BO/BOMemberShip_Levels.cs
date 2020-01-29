using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOMemberShip_Levels {

        MemberShip_Level member = new MemberShip_Level();

        public List<Memberships_levels> getMembers() {
            return member.getMemberShip();
        }

        public Memberships_levels SaveMemberShip(Memberships_levels members) {
            return member.SaveMemberShip(members);
        }

        public Memberships_levels UpdateMemberShip(Memberships_levels members) {
            return member.UpdateMember(members);
        }

        public string DeleteMemberShip(int id) {
            return member.DeleteMember(id);
        }
    }
}
