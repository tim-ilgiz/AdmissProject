using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(asmissAPI.Startup))]
namespace asmissAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
