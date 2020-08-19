using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookItt.Startup))]
namespace BookItt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
