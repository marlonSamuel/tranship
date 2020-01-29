using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class Files {
        public decimal shipment_attachmentId { get; set; }
        public decimal file_typeId { get; set; }
        public decimal shipmentId { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string file_name { get; set; }
        public decimal userId { get; set; }
        public string username { get; set; }
        public DateTime creation_date { get; set; }
        public string file { get; set; }
        public string description { get; set; }
        public decimal? customerId { get; set; }
    }
}