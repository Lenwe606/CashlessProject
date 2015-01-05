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
    public class KassaController : ApiController
    {
        public List<Kassa> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return KassaDA.GetKassas(p.Claims);

        }

        public HttpResponseMessage Post(Kassa c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = KassaDA.InsertKassa(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public HttpResponseMessage Put(Kassa c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            KassaDA.UpdateKassa(c, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            KassaDA.DeleteKassa(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
