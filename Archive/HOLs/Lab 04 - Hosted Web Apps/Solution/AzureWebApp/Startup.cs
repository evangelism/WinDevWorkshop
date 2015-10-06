using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureWebApp.Startup))]
namespace AzureWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
