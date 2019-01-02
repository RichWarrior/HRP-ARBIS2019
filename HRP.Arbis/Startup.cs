using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRP.Arbis.Startup))]
namespace HRP.Arbis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
