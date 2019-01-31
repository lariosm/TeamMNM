using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiscussionProject.Startup))]
namespace DiscussionProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
