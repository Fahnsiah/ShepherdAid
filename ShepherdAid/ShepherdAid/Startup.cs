using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShepherdAid.Startup))]
namespace ShepherdAid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
