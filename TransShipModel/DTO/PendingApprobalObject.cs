using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransShipModel.DTO {
    public class PendingApprobalObject
    {
        public decimal? customerId { get; set; }
        public decimal? userId { get; set; }
        public string username { get; set; }
        public string keyinformation { get; set; }
        public string confirmkey { get; set; }
        public string companyname { get; set; }
        public string fullname { get; set; }
        public string title { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string zipcode { get; set; }
        public long idpais { get; set; }
        public string outsideus { get; set; }
        public string usstate { get; set; }
        public string canadaprovince { get; set; }
        public decimal? membership { get; set; }
        public string city { get; set; }
    }
}
