using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignageDigitalPortal.Startup))]
namespace SignageDigitalPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
