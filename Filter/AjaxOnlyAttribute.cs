using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    /// <summary>
    /// 強制限制只能 AJAX 要求
    /// </summary>
    public class AjaxOnlyAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}