using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gradebook.Startup))]
namespace gradebook
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
