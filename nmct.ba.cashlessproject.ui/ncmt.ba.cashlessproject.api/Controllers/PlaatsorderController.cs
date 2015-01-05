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
    public class PlaatsorderController : ApiController
    {
        public HttpResponseMessage Post(List<string> lijst)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = PlaatsorderDA.PlaatsOrder(lijst, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public HttpResponseMessage Put(List<double> lijst)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            PlaatsorderDA.ChangeSaldo(lijst, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        public HttpResponseMessage Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            double saldo = PlaatsorderDA.GetSaldo(id, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(saldo.ToString());
            return message;
        }
    }
}
