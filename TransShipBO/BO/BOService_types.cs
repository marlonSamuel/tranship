using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOService_types {

        Service_types service = new Service_types();

        public List<service_types> GetServices() {
            return service.getServices();
        }

        public service_types SaveService(service_types serv) {
            return service.SaveService(serv);
        }

        public service_types UpdateService(service_types serv) {
            return service.UpdateService(serv);
        }

        public string DeleteService(int id) {
            return service.DeleteService(id);
        }
    }
}
