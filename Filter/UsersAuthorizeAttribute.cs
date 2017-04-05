using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountBooks.Filter
{
    public class UsersAuthorizeAttribute : AuthorizeAttribute
    {
        public new string[] Roles { get; set; }

        //AuthorizeCore实现验证和授权逻辑，如果这个方法返回true，表示授权成功，如果返回false，
        //表示授权失败, 会给上下文设置一个HttpUnauthorizedResult
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.AuthorizeCore(httpContext);
            if (httpContext == null)
            {
                throw new ArgumentException("HttpContext");
            }
            ////首先判断当前账户是否被认证，如果没有，则返回false
            //if (!httpContext.User.Identity.IsAuthenticated)
            //{  //判断账号是否登录的在Authentication中验证
            //    return false;
            //}
            //如果Action没有指定角色，则执行HttpUnauthorizedResult
            if (Roles == null){
                return true;
            }
            if (Roles.Length> 0)           
            {
                //获取当前账户的类型，并跟给定的类型进行比较，如果类型相同，则返回true，否则返回false
                if (Roles.Any(httpContext.User.IsInRole))
                {
                    return true;
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                   // Array.Clear(Roles, 0, Roles.Length);
                    Roles = null;
                    return false;
                }              
            }
            return false;
        }
        // 如果AuthorizeCore返回false才会执行HandleUnauthorizedRequest
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.HttpContext.Response.StatusCode.Equals(401))
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Error401");
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
            }
            /*
            //验证不通过
            ContentResult Content = new ContentResult();
            Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
            filterContext.Result = Content;
             * */
        }

        //当用户请求一个Action时，会调用OnAuthorization方法
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            string roles = GetRoles.GetActionRoles(actionName, controllerName);

            if (!string.IsNullOrWhiteSpace(roles))
            {
                this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            base.OnAuthorization(filterContext);
        }     

    }
}