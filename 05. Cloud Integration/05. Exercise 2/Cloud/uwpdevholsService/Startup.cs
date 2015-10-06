using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(uwpdevholsService.Startup))]

namespace uwpdevholsService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}