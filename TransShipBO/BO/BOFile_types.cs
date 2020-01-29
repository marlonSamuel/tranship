using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransShipModel;
using TransShipModel.DAO;

namespace TransShipBO.BO {
    public class BOFile_types {
        Type_files file = new Type_files();

        public List<file_types> GetFiles() {
            return file.getFiles();
        }

        public file_types SaveFile(file_types files) {
            return file.SaveFiles(files);
        }

        public file_types UpdateFile(file_types countries) {
            return file.UpdateFiles(countries);
        }

        public string DeleteFile(int id) {
            return file.DeleteFile(id);
        }
    }
}
