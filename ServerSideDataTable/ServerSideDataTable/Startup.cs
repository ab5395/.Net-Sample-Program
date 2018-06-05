using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServerSideDataTable.Startup))]
namespace ServerSideDataTable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
