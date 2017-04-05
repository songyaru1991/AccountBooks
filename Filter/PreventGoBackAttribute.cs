using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    /// <summary>
    /// 偷偷加上 Http Header – 禁止上一頁
    /// </summary>
    public class PreventGoBackAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.HttpContext.Response.Buffer = true;
            filterContext.HttpContext.Response.Expires = -1;
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoServerCaching();
            filterContext.HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
            filterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }
}