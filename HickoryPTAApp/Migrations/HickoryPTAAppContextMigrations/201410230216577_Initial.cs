namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.ServerFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        CommitteeId = c.Int(),
                        PostId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Committees", t => t.CommitteeId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.CommitteeId)
                .Index(t => t.PostId);
            
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
                        CommitteeId = c.Int(),
                        EventDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Location_LocationId = c.Int(),
                        Committee_CommitteeId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeId)
                .ForeignKey("dbo.Committees", t => t.CommitteeId)
                .Index(t => t.CommitteeId)
                .Index(t => t.Location_LocationId)
                .Index(t => t.Committee_CommitteeId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServerFiles", "PostId", "dbo.Posts");
            DropForeignKey("dbo.ChairPersons", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Members", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Posts", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.Posts", "Committee_CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.Posts", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.ChairPersons", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.ServerFiles", "CommitteeId", "dbo.Committees");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "MembershipId" });
            DropIndex("dbo.Members", new[] { "MembershipId" });
            DropIndex("dbo.Posts", new[] { "Committee_CommitteeId" });
            DropIndex("dbo.Posts", new[] { "Location_LocationId" });
            DropIndex("dbo.Posts", new[] { "CommitteeId" });
            DropIndex("dbo.ServerFiles", new[] { "PostId" });
            DropIndex("dbo.ServerFiles", new[] { "CommitteeId" });
            DropIndex("dbo.ChairPersons", new[] { "MemberId" });
            DropIndex("dbo.ChairPersons", new[] { "CommitteeId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.Locations");
            DropTable("dbo.Posts");
            DropTable("dbo.ServerFiles");
            DropTable("dbo.Committees");
            DropTable("dbo.ChairPersons");
        }
    }
}
