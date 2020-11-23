using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizManager.UI.Startup))]
namespace QuizManager.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
