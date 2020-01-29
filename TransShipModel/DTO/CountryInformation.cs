using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransShipModel.DTO {
    public class CountryInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LongCountryName { get; set; }
        public string ISO3 { get; set; }
        public string ISO2 { get; set; }
        public string NumCode { get; set; }
        public string UNMemberState { get; set; }
        public string CallingCode { get; set; }
        public string CCTLD { get; set; }
    }
}
