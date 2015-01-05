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
    public class KlantVerkrijgenController : ApiController
    {
        public Klant Get(int code)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return KlantVerkrijgenDA.GetKlant(p.Claims,code);
        }

        public HttpResponseMessage Put(List<double> lijst)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            KlantVerkrijgenDA.UpdateSaldo(lijst, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Klant c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = KlantVerkrijgenDA.InsertKlant(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }
    }
}
