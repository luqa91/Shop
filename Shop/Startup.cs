using Hangfire;
using Hangfire.SqlServer;
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
            JobStorage.Current = new SqlServerStorage("ProductsContext");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.MapSignalR();

        }


    }
}