using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iTravels.Web.Startup))]
namespace iTravels.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}