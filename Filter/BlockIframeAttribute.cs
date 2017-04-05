using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    public class BlockIframeAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 偷偷加上 Http Header – 禁止被iFrame
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if (!filterContext.IsChildAction)
            {
                filterContext
                .HttpContext
                .Response
                .AppendHeader("X-Frame-Options", "SAMEORIGIN");
            }
        }
    }
}