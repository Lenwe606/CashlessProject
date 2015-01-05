using Lib;
using ncmt.ba.cashlessproject.api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ncmt.ba.cashlessproject.api.Controllers
{
    public class MedewerkerController : ApiController
    {
        public List<Medewerker> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return MedewerkerDA.GetMedewerkers(p.Claims);

        }

        public HttpResponseMessage Post(Medewerker c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = MedewerkerDA.InsertMedewerker(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public HttpResponseMessage Put(Medewerker c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            MedewerkerDA.UpdateMedewerker(c, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            MedewerkerDA.DeleteMedewerker(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
