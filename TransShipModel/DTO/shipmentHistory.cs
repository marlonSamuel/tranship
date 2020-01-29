using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class shipmentHistory {
        public decimal shipmentId { get; set; }
        public decimal customerId { get; set; }
        public decimal serviceTypeId { get; set; }
        public string purcharse_order { get; set; }
        public string consignee_name { get; set; }
        public DateTime shipment_date { get; set; }
        public string description { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public decimal total { get; set; }
    }
}