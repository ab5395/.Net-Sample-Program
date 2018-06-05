using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatatablesExample.Startup))]
namespace DatatablesExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
