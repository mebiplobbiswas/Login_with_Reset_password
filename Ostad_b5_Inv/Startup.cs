using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ostad_b5_Inv.Startup))]
namespace Ostad_b5_Inv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
