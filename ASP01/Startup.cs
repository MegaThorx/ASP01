using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP01.Startup))]
namespace ASP01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
