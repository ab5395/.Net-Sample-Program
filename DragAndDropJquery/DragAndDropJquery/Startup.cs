using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DragAndDropJquery.Startup))]
namespace DragAndDropJquery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
