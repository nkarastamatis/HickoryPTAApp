namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommitteePosts",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        PostTitle = c.String(),
                        PostBody = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        EventDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PostId, t.CommitteeId })
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .Index(t => t.CommitteeId);
            
            CreateTable(
                "dbo.PostFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false),
                        EntryId = c.Int(nullable: false),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        CommitteePost_PostId = c.Int(),
                        CommitteePost_CommitteeId = c.Int(),
                    })
                .PrimaryKey(t => new { t.FileId, t.EntryId })
                .ForeignKey("dbo.CommitteePosts", t => new { t.CommitteePost_PostId, t.CommitteePost_CommitteeId })
                .Index(t => new { t.CommitteePost_PostId, t.CommitteePost_CommitteeId });
            
            CreateTable(
                "dbo.CommitteeFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false),
                        CommitteeId = c.Int(nullable: false),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => new { t.FileId, t.CommitteeId })
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .Index(t => t.CommitteeId);
            
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
                        MembershipId = c.String(nullable: false),
                        Membership_MembershipId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Memberships", t => t.Membership_MembershipId)
                .Index(t => t.Membership_MembershipId);
            
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
                        MembershipId = c.String(nullable: false),
                        TeacherId = c.String(nullable: false),
                        Membership_MembershipId = c.Int(),
                        Teacher_TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Memberships", t => t.Membership_MembershipId)
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherId)
                .Index(t => t.Membership_MembershipId)
                .Index(t => t.Teacher_TeacherId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommitteePosts", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.ChairPersons", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "Membership_MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Members", "Membership_MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.ChairPersons", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeFiles", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" }, "dbo.CommitteePosts");
            DropIndex("dbo.Students", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.Students", new[] { "Membership_MembershipId" });
            DropIndex("dbo.Members", new[] { "Membership_MembershipId" });
            DropIndex("dbo.ChairPersons", new[] { "MemberId" });
            DropIndex("dbo.ChairPersons", new[] { "CommitteeId" });
            DropIndex("dbo.CommitteeFiles", new[] { "CommitteeId" });
            DropIndex("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" });
            DropIndex("dbo.CommitteePosts", new[] { "CommitteeId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.ChairPersons");
            DropTable("dbo.Committees");
            DropTable("dbo.CommitteeFiles");
            DropTable("dbo.PostFiles");
            DropTable("dbo.CommitteePosts");
        }
    }
}
