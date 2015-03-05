using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CartPhill.Startup))]
namespace CartPhill
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
