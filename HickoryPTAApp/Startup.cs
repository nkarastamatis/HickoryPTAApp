using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HickoryPTAApp.Startup))]
namespace HickoryPTAApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
