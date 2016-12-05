using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERPAnimalia.Startup))]
namespace ERPAnimalia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
