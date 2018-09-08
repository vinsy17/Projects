using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace iTravels.Web
{
    public class iTAuthorizeAttribute : AuthorizeAttribute
    {
        //
        // Summary:
        //     Called when a process requests authorization.
        //
        // Parameters:
        //   filterContext:
        //     The filter context, which encapsulates information for using System.Web.Mvc.AuthorizeAttribute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The filterContext parameter is null.
        public override void OnAuthorization(AuthorizationContext filterContext) {
            //if (filterContext.HttpContext.Session!=null)// HttpContext.Current.Session == null)
            //{

            //}
        }
        //
        // Summary:
        //     When overridden, provides an entry point for custom authorization checks.
        //
        // Parameters:
        //   httpContext:
        //     The HTTP context, which encapsulates all HTTP-specific information about an individual
        //     HTTP request.
        //
        // Returns:
        //     true if the user is authorized; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The httpContext parameter is null.
        //protected override bool AuthorizeCore(HttpContextBase httpContext) { }
        //
        // Summary:
        //     Processes HTTP requests that fail authorization.
        //
        // Parameters:
        //   filterContext:
        //     Encapsulates the information for using System.Web.Mvc.AuthorizeAttribute. The
        //     filterContext object contains the controller, HTTP context, request context,
        //     action result, and route data.
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;//filterContext.HttpContext.Session
            // check if session is supported  
            if (ctx.Session != null)
            {
                // check if a new session id was generated  
                if (ctx.Session["UserId"] == null || ctx.Session.IsNewSession)
                {
                    //Check is Ajax request  
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.ClearContent();
                        filterContext.HttpContext.Items["AjaxPermissionDenied"] = true;
                    }
                    // check if a new session id was generated  
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Account/Login");
                    }
                }
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
        //
        // Summary:
        //     Called when the caching module requests authorization.
        //
        // Parameters:
        //   httpContext:
        //     The HTTP context, which encapsulates all HTTP-specific information about an individual
        //     HTTP request.
        //
        // Returns:
        //     A reference to the validation status.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The httpContext parameter is null.
        //protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext) { }

    }
    public class iAuthenticateAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //if (HttpContext.Current.Session == null)
            //{

            //}
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated || HttpContext.Current.Session==null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}