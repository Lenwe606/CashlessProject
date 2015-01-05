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
    public class DrankenController : ApiController
    {
        public List<Product> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return ProductenDA.GetDranken(p.Claims);

        }
    }
}
