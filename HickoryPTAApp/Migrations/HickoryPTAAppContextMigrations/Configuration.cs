namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PTAData.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<HickoryPTAApp.Models.HickoryPTAAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\HickoryPTAAppContextMigrations";
        }

        protected override void Seed(HickoryPTAApp.Models.HickoryPTAAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            
            using (var committeeRepo = new HickoryPTAApp.Models.CommitteeRepository())
            {
                var globalPtaCommittee = committeeRepo.GlobalPtaCommittee();
            }

            using (var membershipRepo = new HickoryPTAApp.Models.MemberRepository())
            {
                var defaultAdminMember = membershipRepo.DefaultAdminMember();
            }
        }
    }
}
