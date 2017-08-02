using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieScrapper.Startup))]
namespace MovieScrapper
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
