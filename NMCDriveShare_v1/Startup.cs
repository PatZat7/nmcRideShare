using Microsoft.Owin;
using System.Data.Entity;
using Owin;

[assembly: OwinStartupAttribute(typeof(NMCDriveShare_v1.Startup))]
namespace NMCDriveShare_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

			// add SignalR to the authentication system
			app.MapSignalR();
        }
    }
}
