using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class Payments {
        public decimal userId { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string userType { get; set; }
        public string Email { get; set; }
        public decimal membershipId { get; set; }
        public decimal customerId { get; set; }
        public string description { get; set; }
        public string company_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public decimal countryId { get; set; }
        public DateTime? expired { get; set; }
        public string zipcode {get; set; }
        public string contactName { get; set; }
        public string status { get; set; }
        public string logo { get; set; }
    }
}