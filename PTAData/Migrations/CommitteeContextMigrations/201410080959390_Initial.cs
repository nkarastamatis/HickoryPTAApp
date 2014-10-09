namespace PTAData.Migrations.CommitteeContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommitteeEntries",
                c => new
                    {
                        EntryId = c.String(nullable: false, maxLength: 128),
                        CommitteeId = c.String(maxLength: 128),
                        EntryTitle = c.String(),
                        EntryBody = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.EntryId)
                .ForeignKey("dbo.Committees", t => t.CommitteeId)
                .Index(t => t.CommitteeId);
            
            CreateTable(
                "dbo.EntryFiles",
                c => new
                    {
                        FileId = c.String(nullable: false, maxLength: 128),
                        EntryId = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        Path = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => new { t.FileId, t.EntryId })
                .ForeignKey("dbo.CommitteeEntries", t => t.EntryId, cascadeDelete: true)
                .Index(t => t.EntryId);
            
            CreateTable(
                "dbo.CommitteeFiles",
                c => new
                    {
                        FileId = c.String(nullable: false, maxLength: 128),
                        CommitteeId = c.String(nullable: false, maxLength: 128),
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
                        CommitteeId = c.String(nullable: false, maxLength: 128),
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
                        CommitteeId = c.String(nullable: false, maxLength: 128),
                        MemberId = c.String(nullable: false, maxLength: 128),
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
                        MemberId = c.String(nullable: false, maxLength: 128),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        MembershipId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Memberships", t => t.MembershipId, cascadeDelete: true)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        MembershipId = c.String(nullable: false, maxLength: 128),
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
                        StudentId = c.String(nullable: false, maxLength: 128),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        MembershipId = c.String(nullable: false),
                        TeacherId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Memberships", t => t.StudentId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        Name_First = c.String(),
                        Name_Last = c.String(),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommitteeEntries", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.ChairPersons", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "StudentId", "dbo.Memberships");
            DropForeignKey("dbo.Members", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.ChairPersons", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.CommitteeFiles", "CommitteeId", "dbo.Committees");
            DropForeignKey("dbo.EntryFiles", "EntryId", "dbo.CommitteeEntries");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "StudentId" });
            DropIndex("dbo.Members", new[] { "MembershipId" });
            DropIndex("dbo.ChairPersons", new[] { "MemberId" });
            DropIndex("dbo.ChairPersons", new[] { "CommitteeId" });
            DropIndex("dbo.CommitteeFiles", new[] { "CommitteeId" });
            DropIndex("dbo.EntryFiles", new[] { "EntryId" });
            DropIndex("dbo.CommitteeEntries", new[] { "CommitteeId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.ChairPersons");
            DropTable("dbo.Committees");
            DropTable("dbo.CommitteeFiles");
            DropTable("dbo.EntryFiles");
            DropTable("dbo.CommitteeEntries");
        }
    }
}
