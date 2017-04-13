using Hangfire;
using Microsoft.Owin;
using Owin;



[assembly: OwinStartup(typeof(Shop.Startup))]

namespace Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {


            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("ProductsContext");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
 

        }


    }
}