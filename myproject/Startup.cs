using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myproject.Startup))]
namespace myproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
