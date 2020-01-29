using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransShipModel.Responses
{
    [Serializable]
    public class ErrorMessage : Message
    {
        // Class used to have an error description  
        // this is a response that contains exceptions or errors thrown by the api.

        Error error;

        public ErrorMessage(Error error)
        {
            this.error = error;
        }

        public ErrorMessage(string status, string title, string detail)
        {
            this.error = new Error();
            error.status = status;
            error.title = title;
            error.detail = detail;
        }
    }
}