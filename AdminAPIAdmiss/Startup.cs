using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminAPIAdmiss.Startup))]
namespace AdminAPIAdmiss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
