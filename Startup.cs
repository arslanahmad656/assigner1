using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assigner.Startup))]
namespace Assigner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
