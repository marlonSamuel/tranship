using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TransShipBO.BO;
using TransShipModel;
using TransShipModel.Responses;

namespace TransShip.Controllers
{
    public class MemberShip_LevelController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
        public HttpResponseMessage Get() {
            try {

                BOMemberShip_Levels member = new BOMemberShip_Levels();

                var listMember = new List<Memberships_levels>();
                listMember = member.getMembers();
                return Request.CreateResponse(HttpStatusCode.OK, listMember);

            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to get memberShip levels - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Post(Memberships_levels model) {
            try {
                BOMemberShip_Levels member = new BOMemberShip_Levels();
                Memberships_levels memb;

                memb = member.SaveMemberShip(model);
                return Request.CreateResponse(HttpStatusCode.OK, memb);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to save countrie - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Put(Memberships_levels model) {
            try {
                BOMemberShip_Levels member = new BOMemberShip_Levels();
                Memberships_levels memb;

                memb = member.UpdateMemberShip(model);
                return Request.CreateResponse(HttpStatusCode.OK, memb);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update memberShip levels - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                BOMemberShip_Levels member = new BOMemberShip_Levels();

                var delete = member.DeleteMemberShip(id);

                return Request.CreateResponse(HttpStatusCode.OK, delete);
            } catch (Exception e) {
                ErrorMessage mensaje = new ErrorMessage("2.1", "Exception to update memberShip levels - " + e.GetBaseException().Message, e.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }

        }
    }
}
