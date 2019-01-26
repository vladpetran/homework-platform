using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EducationalPortal
{
    public class RoleAuthorize : AuthorizeAttribute  
    {
        private readonly string[] allowedroles;

        public RoleAuthorize(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach (var role in allowedroles)
            {
                if (httpContext.Request.Cookies["UserSettings"] != null)
                {
                    string UserRole = httpContext.Request.Cookies["UserSettings"].Values["Role"];
                    if (role == UserRole)
                    {
                        authorize = true;
                    }
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { {"action","Index"},
            {"controller","Account"}});
            
        }
    }
}