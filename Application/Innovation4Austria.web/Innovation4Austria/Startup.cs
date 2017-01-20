using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Innovation4Austria.Startup))]
namespace Innovation4Austria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
