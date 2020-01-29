using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.Responses
{
    public class Error
    {
        public string status { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
    }
}