using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PTAData.Entities;

namespace HickoryPTAApp.Models
{
    public class HickoryPTAAppContext : DbContext
    {

        public HickoryPTAAppContext()
            : base("DefaultConnection")
        {
        }
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<HickoryPTAApp.Models.HickoryPTAAppContext>());

        //public DbSet<PTAData.Entities.Member> Members { get; set; }

        //public DbSet<PTAData.Entities.Membership> Memberships { get; set; }

        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeFile> CommitteeFiles { get; set; }
        public DbSet<CommitteePost> CommitteeEntries { get; set; }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}