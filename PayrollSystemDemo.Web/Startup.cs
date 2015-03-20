using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayrollSystemDemo.Web.Startup))]
namespace PayrollSystemDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
