using System.Collections.Generic;
using System.Web.Http;
using TransShip.Models;
using System.Linq;

namespace TransShip.Controllers
{
    public class ValidationsController : ApiController
    {
        [HttpPost]
        public int PostResponse(Usuario response)
        {
            LogBook.LogBook.TextLog.Info(string.Format("User Name {0}", response.Email));            
            var valorRetorno = false;
            if (ModelState.IsValid)
            {
                valorRetorno = Repository.Responses.Where(
                    x => x.Email.Equals(response.Email, System.StringComparison.InvariantCultureIgnoreCase) && x.Password.Equals(response.Password, System.StringComparison.InvariantCultureIgnoreCase)
                    ).Any();
                
            }
            return valorRetorno ? 1 : 0;
        }
    }
}