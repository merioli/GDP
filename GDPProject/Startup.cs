using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDPProject.Startup))]
namespace GDPProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
