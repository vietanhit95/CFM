using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cfm.Web.Mvc.Startup))]
namespace Cfm.Web.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
