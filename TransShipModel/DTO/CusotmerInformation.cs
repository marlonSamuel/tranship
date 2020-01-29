using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransShipModel.DTO {
    public class CusotmerInformation
    {
        public decimal CustomerId { get; set; }
        public string CompanyName { get; set; }
        public decimal UserId { get; set; }
        public string phone_number { get; set; }
        public DateTime? register_date { get; set; }
        public string contact_name { get; set; }
    }
}
