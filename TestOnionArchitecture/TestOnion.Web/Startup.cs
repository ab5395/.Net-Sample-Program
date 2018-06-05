using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestOnion.Web.Startup))]
namespace TestOnion.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
