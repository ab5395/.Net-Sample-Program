using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayoutAplication.Startup))]
namespace PayoutAplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
