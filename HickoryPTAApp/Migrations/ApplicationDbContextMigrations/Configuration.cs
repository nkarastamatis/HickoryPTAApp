namespace HickoryPTAApp.Migrations.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<HickoryPTAApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\ApplicationDbContextMigrations";
            ContextKey = "HickoryPTAApp.Models.ApplicationDbContext";
        }

        protected override void Seed(HickoryPTAApp.Models.ApplicationDbContext context)
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
            //

            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public void InitializeIdentityForEF(HickoryPTAApp.Models.ApplicationDbContext context)
        {
            using (var UserManager = new UserManager<HickoryPTAApp.Models.ApplicationUser>(
                new UserStore<HickoryPTAApp.Models.ApplicationUser>(context)))
            using (var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
            {
                string username = "Admin";
                string password = "Hickory1";
                string role = HickoryPTAApp.Models.AdminConstants.Roles.Administrator;

                //Create Role Administrator if it does not exist
                if (!RoleManager.RoleExists(role))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(role));
                }

                // Create User=Admin with password=123456
                var user = new HickoryPTAApp.Models.ApplicationUser();
                user.UserName = username;

                using (var membershipRepo = new HickoryPTAApp.Models.MemberRepository())
                {
                    var defaultAdminMember = membershipRepo.DefaultAdminMember();
                    user.MemberId = defaultAdminMember.MemberId;
                    user.Email = defaultAdminMember.Email;
                }

                var adminresult = UserManager.Create(user, password);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, role);
                }
            }
        }
    }
}
