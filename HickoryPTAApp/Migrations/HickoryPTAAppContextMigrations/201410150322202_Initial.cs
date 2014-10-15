namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostTitle = c.String(),
                        PostBody = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeId = c.Int(nullable: false, identity: true),
                        CommitteeName = c.String(),
                        Description = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.CommitteeId);
            
            CreateTable(
                "dbo.CommitteeFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        CommitteeId = c.Int(nullable: false),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .Index(t => t.CommitteeId);
            
            CreateTable(
                "dbo.ChairPersons",
                c => new
                    {
                        CommitteeId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommitteeId, t.MemberId })
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.CommitteeId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        MembershipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Memberships", t => t.MembershipId, cascadeDelete: true)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Address_StreetAddress = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_Zip = c.String(),
                    })
                .PrimaryKey(t => t.MembershipId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        MembershipId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Memberships", t => t.MembershipId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.MembershipId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            CreateTable(
                "dbo.PostFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_StreetAddress = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_Zip = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.CommitteePosts",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.CommitteeId);
            
            CreateTable(
                "dbo.CommitteeEvents",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        Committee_CommitteeId = c.Int(),
                        Location_LocationId = c.Int(),
                        EventDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.CommitteePosts", t => t.PostId)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.PostId)
                .Index(t => t.Committee_CommitteeId)
                .Index(t => t.Location_LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommitteeEvents", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.CommitteeEvents", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeEvents", "PostId", "dbo.CommitteePosts");
            DropForeignKey("dbo.CommitteePosts", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteePosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostFiles", "PostId", "dbo.Posts");
            DropForeignKey("dbo.ChairPersons", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Members", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.ChairPersons", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeFiles", "CommitteeId", "dbo.Committees");
            DropIndex("dbo.CommitteeEvents", new[] { "Location_LocationId" });
            DropIndex("dbo.CommitteeEvents", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.CommitteeEvents", new[] { "PostId" });
            DropIndex("dbo.CommitteePosts", new[] { "CommitteeId" });
            DropIndex("dbo.CommitteePosts", new[] { "PostId" });
            DropIndex("dbo.PostFiles", new[] { "PostId" });
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "MembershipId" });
            DropIndex("dbo.Members", new[] { "MembershipId" });
            DropIndex("dbo.ChairPersons", new[] { "MemberId" });
            DropIndex("dbo.ChairPersons", new[] { "CommitteeId" });
            DropIndex("dbo.CommitteeFiles", new[] { "CommitteeId" });
            DropTable("dbo.CommitteeEvents");
            DropTable("dbo.CommitteePosts");
            DropTable("dbo.Locations");
            DropTable("dbo.PostFiles");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.ChairPersons");
            DropTable("dbo.CommitteeFiles");
            DropTable("dbo.Committees");
            DropTable("dbo.Posts");
        }
    }
}
