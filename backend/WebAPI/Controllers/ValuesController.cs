using System.Web.Http;

namespace Damasio34.SGP.WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        [Authorize]
        public string Get()
        {
            return User.Identity.Name;
        }
    }
}