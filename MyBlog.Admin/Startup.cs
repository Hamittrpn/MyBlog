using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBlog.Admin.Startup))]
namespace MyBlog.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
