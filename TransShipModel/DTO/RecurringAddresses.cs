using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class RecurringAddresses {
        public decimal customerId { get; set; }
        public decimal? addressId { get; set; }
        public Nullable<long> countryId { get; set; }
        public string Country_Name { get; set; }
        public string consignee_name { get; set; }
        public string target_company { get; set; }
        public string phone_number { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string zipcode { get; set; }
    }
}