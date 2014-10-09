using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace PTAData.Migrations
{
    public class InternalMigrator
    {
        public static void Update()
        {
            var membershipContextConfiguration = new PTAData.Migrations.MembershipContextMigrations.Configuration();
            var migrator = new DbMigrator(membershipContextConfiguration);
            migrator.Update();

            var applicationUserConfiguration = new PTAData.Migrations.ApplicationUserContextMigrations.Configuration();
            migrator = new DbMigrator(applicationUserConfiguration);
            //migrator.Update();

            //var committeeContextConfiguration = new PTAData.Migrations.CommitteeContextMigrations.Configuration();
            //migrator = new DbMigrator(committeeContextConfiguration);
            //migrator.Update();
        }
    }
}
