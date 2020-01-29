using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransShipModel.Abstract;

namespace TransShipModel.Responses
{
    [Serializable]
    public class DataMessage: Message
    {
        List<DICObject> data;
        public DataMessage(List<DICObject> data)
        {
            this.data = data;
        }

        public DataMessage(DICObject row)
        {
            this.data = new List<DICObject>();
            this.data.Add(row);
        }
    }
}