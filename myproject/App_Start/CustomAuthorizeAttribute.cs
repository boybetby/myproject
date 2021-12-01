using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myproject.App_Start
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string ViewName { get; set; }
        public CustomAuthorizeAttribute()
        {
            ViewName = "AuthorizeFailed";
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }
        void IsUserAuthorized(AuthorizationContext authorizationContext)
        {
            if (authorizationContext.Result == null)
                return;
            if (authorizationContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewDataDictionary dic = new ViewDataDictionary();
                dic.Add("Message", "You don't have permission for this operation !");
                var result = new ViewResult() { ViewName = this.ViewName, ViewData = dic};
                authorizationContext.Result = result;
            }
        }
    }
}