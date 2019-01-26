using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EducationalPortal
{
    public class ConfirmAuthorize: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (httpContext.Request.Cookies["UserSettings"] != null && httpContext.Request.Cookies["UserSettings"].Values["Confirmed"]=="Yes")
            {
                authorize = true;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { {"action","Index"},
            {"controller","Home"}});

        }
    }
}