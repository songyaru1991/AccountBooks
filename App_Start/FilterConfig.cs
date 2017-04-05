using AccountBooks.Filter;
using System.Web.Mvc;

namespace IdentitySample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //注册AuthenticationAttribute的特性过滤器：
            filters.Add(new AuthenticationAttribute());
            filters.Add(new NoAuthenticationAttribute());

            filters.Add(new UsersAuthorizeAttribute());
        }
    }
}
