using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(URent.Startup))]
namespace URent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
