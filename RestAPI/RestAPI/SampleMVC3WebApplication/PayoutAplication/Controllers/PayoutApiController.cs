using PayoutAplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PayoutAplication.Controllers
{
    [AllowAnonymous]
    public class PayoutApiController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetData([FromBody] RootObject test)
        {
            return null;
        }
    }
}
