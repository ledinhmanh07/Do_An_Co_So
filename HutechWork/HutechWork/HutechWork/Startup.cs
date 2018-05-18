using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HutechWork.Startup))]
namespace HutechWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
