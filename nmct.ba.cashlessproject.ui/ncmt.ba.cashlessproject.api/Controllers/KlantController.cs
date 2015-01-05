using Lib;
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
    public class KlantController : ApiController
    {
        public List<Klant> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return KlantDA.GetKlanten(p.Claims);

        }

        public HttpResponseMessage Put(Klant c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            KlantDA.UpdateKlant(c, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            KlantDA.DeleteKlant(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
