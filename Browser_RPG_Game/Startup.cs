using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Browser_RPG_Game.Startup))]
namespace Browser_RPG_Game
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
