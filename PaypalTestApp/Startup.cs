using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaypalTestApp.Startup))]
namespace PaypalTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
