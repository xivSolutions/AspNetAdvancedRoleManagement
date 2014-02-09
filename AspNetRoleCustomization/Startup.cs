using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetRoleCustomization.Startup))]
namespace AspNetRoleCustomization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
