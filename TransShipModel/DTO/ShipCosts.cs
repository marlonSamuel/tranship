using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class ShipCosts {
        public decimal shipmentId { get; set; }
        public decimal service_typeId { get; set; }
        public decimal measure_unitId { get; set; }
        public decimal quantity { get; set; }
        public decimal? unit_price { get; set; }
        public string type {get; set;}
        public string description {get; set;}
        public decimal? totalbyreg {get; set;}
        public decimal? totalCosts {get; set;}
        public decimal total {get; set;}
        public string vendor { get; set; }

    }
}