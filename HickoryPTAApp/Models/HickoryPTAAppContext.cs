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
        public DbSet<ServerFile> ServerFiles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }

        public DbSet<Committee> Committees { get; set; }
        public DbSet<ChairPerson> CommitteeChairs { get; set; }
        public DbSet<CommitteeFile> CommitteeFiles { get; set; }
        public DbSet<CommitteePost> CommitteePosts { get; set; }
        public DbSet<CommitteeEvent> CommitteeEvents { get; set; }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PostFile>().ToTable("PostFiles");
            //modelBuilder.Entity<Post>().ToTable("Posts");

            //modelBuilder.Entity<Committee>().ToTable("Committees");
            //modelBuilder.Entity<CommitteeFile>().ToTable("CommitteeFiles");
            //modelBuilder.Entity<CommitteePost>().ToTable("CommitteePosts");
            //modelBuilder.Entity<CommitteeEvent>().ToTable("CommitteeEvents");

        }
    }

}