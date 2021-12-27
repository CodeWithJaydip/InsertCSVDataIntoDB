using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsertCSVDataIntoDB.Startup))]
namespace InsertCSVDataIntoDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
