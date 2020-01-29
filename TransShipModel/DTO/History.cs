using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class History {
        public decimal membership_HistoryId {get; set;}
        public decimal customerId { get; set; }
        public DateTime initial_date { get; set; }
        public DateTime payment_date { get; set; }
        public DateTime final_date { get; set; }
        public decimal membershipId { get; set; }
        public string description { get; set; }
    }
}