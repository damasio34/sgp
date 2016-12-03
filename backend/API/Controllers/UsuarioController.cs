using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Damasio34.SGP.API.Controllers
{
    public class UsuarioController : ApiController
    {
        [Route("api/security/token/logout")]
        public HttpResponseMessage Logout()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer, DefaultAuthenticationTypes.ApplicationCookie);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
