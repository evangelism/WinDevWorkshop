using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UWPHolsService.Startup))]

namespace UWPHolsService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}