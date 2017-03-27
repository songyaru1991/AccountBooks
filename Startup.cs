using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountBooks.Startup))]
namespace AccountBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
