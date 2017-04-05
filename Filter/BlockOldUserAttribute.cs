using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    /// <summary>
    /// 設定預設的 ViewData
    /// </summary>
    public class BlockOldUserAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["test"] = "demo";
        }
    }
}