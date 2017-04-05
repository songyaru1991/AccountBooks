using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class,AllowMultiple=false,Inherited=true)]
    public sealed class NoAuthenticationAttribute:ActionFilterAttribute
    {
         public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
         base.OnActionExecuting(filterContext);
        }
    }
}
