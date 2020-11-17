using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.MainApplication.WebUtility;

namespace Web.MainApplication.Controllers.Api
{
    [AplicationAuthorizationFilterApi]
    public class BaseController : ApiController
    {
    }
}
