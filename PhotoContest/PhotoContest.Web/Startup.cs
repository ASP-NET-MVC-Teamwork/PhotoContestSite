using Microsoft.Owin;

[assembly: OwinStartup(typeof(PhotoContest.Web.Startup))]
namespace PhotoContest.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
