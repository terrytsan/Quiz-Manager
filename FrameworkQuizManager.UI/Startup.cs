using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrameworkQuizManager.UI.Startup))]

namespace FrameworkQuizManager.UI
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