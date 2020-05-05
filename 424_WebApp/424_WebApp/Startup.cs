using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_424_WebApp.Startup))]
namespace _424_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
