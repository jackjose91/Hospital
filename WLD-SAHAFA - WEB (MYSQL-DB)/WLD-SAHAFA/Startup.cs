using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WLD_SAHAFA.Startup))]
namespace WLD_SAHAFA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
