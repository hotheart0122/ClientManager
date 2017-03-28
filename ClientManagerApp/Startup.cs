using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientManagerApp.Startup))]
namespace ClientManagerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
