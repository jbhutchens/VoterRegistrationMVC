using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VoterRegistrationMVC.Startup))]
namespace VoterRegistrationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
