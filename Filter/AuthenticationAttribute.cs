using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace AccountBooks.Filter
{
    public class AuthenticationAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(NoAuthenticationAttribute), true))
            {
                if (filterContext.HttpContext.Session["userName"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                }

                /*
                //如果不存在身份信息
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ContentResult Content = new ContentResult();
                    Content.Content = string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>", FormsAuthentication.LoginUrl);
                    filterContext.Result = Content;
                }
                 * */
            }
            base.OnActionExecuting(filterContext);
        }
    }
}