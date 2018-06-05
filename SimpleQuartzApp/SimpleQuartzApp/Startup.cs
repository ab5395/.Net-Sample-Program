using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleQuartzApp.Startup))]
namespace SimpleQuartzApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
