using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
namespace Web.MainApplication.WebUtility
{
    public class AuthorizeAttributeSwitch : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorized = base.IsAuthorized(actionContext);
            if (!authorized)
            {
                // log the denied access attempt.
            }
            return authorized;
        }
    }
}