using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;
using TransShipModel.DTO;

namespace TransShipBO.BO {
    public class BOUsers {

        Users user = new Users();

        public List<users> GetUsers() {
            return user.getUsers();
        }

        public List<users> GetUserId(int id) {
            return user.getUserId(id);
        }

        public users getUserByUserId(int idUser) {
            return user.getUserByUserId(idUser);
        }

        public users SaveUser(users users) {
            return user.SaveUser(users);
        }

        public users UpdateUser(users users) {
            return user.UpdateUser(users);
        }

        public string DeleteUser(int id) {
            return user.DeleteUser(id);
        }

        public Payments getUser(string email) {
            return user.getUser(email);
        }

        public customers updateProfile(customers model) {
            return user.UpdateProfile(model);
        }

        public string changePassword(int id, string email, string password, string newPass) {
            return user.ChangePassword(id, email, password, newPass);
        }
    }
}
