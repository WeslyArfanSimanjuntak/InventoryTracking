using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;
using Web.MainApplication.Controllers.Api;
using Web.MainApplication.Resources;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Web.MainApplication.WebUtility
{
    public class AplicationAuthorizationFilterApi : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var user = ((BaseController)actionContext.ControllerContext.Controller).User;

            var roles = ((ClaimsIdentity)user.Identity).FindAll(ClaimTypes.Role).ToList().Select(x => x.Value.ToLower()).ToList();
            string actionName = actionContext.ControllerContext.RouteData.Values["action"]?.ToString().ToLower();
            string controllerName = actionContext.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            var unauthorizedRole = SystemResources.UnauthorizedRole.ToLower().Split(';').ToList();

            if (!roles.Contains(controllerName + "/" + actionName))
            {
                var content = new { message = "Unauthorized", payload = "", response = "401" };
                HttpResponseMessage responseMessage = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent(JsonConvert.SerializeObject(content))
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }
            //if (user.Identity.IsAuthenticated == false)
            //{
            //    if (unauthorizedRole.Contains(controllerName + "/" + actionName) == false)
            //    {
            //        actionContext.Response = new HttpUnauthorizedResult();
            //    }


            //}
            //else
            //{
            //    if (!roles.Contains("api/" + controllerName + "/" + actionName))
            //    {
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "UnAuthorized" }));
            //    }
            //}



        }
        private void AddDefaultRoleToUser(out List<string> roles)
        {
            roles = new List<string>();
            roles = SystemResources.DefaultRole.ToLower().Split(';').ToList();
        }
        public void OnAuthentication(HttpActionContext filterContext)
        {
            //filterContext.Result = new RedirectResult("~/Content/RangeError.html");
            //var temp = ((HomeController)filterContext.Controller).User.Identity;
        }
        public void OnAuthenticationChallenge(HttpActionContext filterContext)
        {
            //Your Code
        }

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    var controller = (BaseController)filterContext.Controller;

        //    controller.TempData["MessageToViewError"] = controller.MessageToViewError;
        //    controller.TempData["MessageToViewInfo"] = controller.MessageToViewInfo;
        //    controller.TempData["MessageToViewSuccess"] = controller.MessageToViewSuccess;
        //    controller.TempData["MessageToViewWarning"] = controller.MessageToViewWarning;
        //    controller.ClearMessage();
        //}
    }
}