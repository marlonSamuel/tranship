using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class Notes {
        public decimal shipmentId { get; set; }
        public decimal shipment_noteId { get; set; }
        public decimal userId { get; set; }
        public string note { get; set; }
        public string status { get; set; }
        public DateTime creation_date { get; set; }
        public string username { get; set; }
    }
}