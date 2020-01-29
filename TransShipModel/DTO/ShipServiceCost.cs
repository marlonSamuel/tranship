using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.DTO {
    public class ShipServiceCost {
        public decimal shipmentId { get; set; }
        public decimal service_typeId { get; set; }
        public decimal measure_unitId { get; set; }
        public decimal quantity { get; set; }
        public decimal? estimated_unit_price { get; set; }
        public decimal? confirmed_unit_price { get; set; }
        public string type {get; set;}
        public string description {get; set;}
        public decimal? totalbyreg {get; set;}
        public decimal totalServices {get; set;}
        public decimal total {get; set;}
    }
}