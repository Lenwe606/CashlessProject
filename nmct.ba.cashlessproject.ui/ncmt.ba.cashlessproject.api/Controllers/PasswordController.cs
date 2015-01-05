using ncmt.ba.cashlessproject.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ncmt.ba.cashlessproject.api.Controllers
{
    public class PasswordController : ApiController
    {
        public List<string> Get(string user)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return PasswordDA.GetInfo(user, p.Claims);

        }

        public HttpResponseMessage Put(List<string> lijst)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            PasswordDA.UpdatePass(lijst, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
