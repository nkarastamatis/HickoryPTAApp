using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Migrations;

namespace HickoryPTAApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders[typeof(DateTime)] =
                new PTAData.DateAndTimeModelBinder() { Date = "Date", Time = "Time" };

            var applicationContextConfiguration = new HickoryPTAApp.Migrations.ApplicationDbContextMigrations.Configuration();
            var migrator = new DbMigrator(applicationContextConfiguration);
            migrator.Update();

            var hickoryPtaAppContextConfiguration = new HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations.Configuration();
            migrator = new DbMigrator(hickoryPtaAppContextConfiguration);
            migrator.Update();
        }
    }
}
