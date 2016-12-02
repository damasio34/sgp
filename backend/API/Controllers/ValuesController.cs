using System.Collections.Generic;
using System.Web.Http;

namespace Damasio34.SGP.API.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }    
    }
}
