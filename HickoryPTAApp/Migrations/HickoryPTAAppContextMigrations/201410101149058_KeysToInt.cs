namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeysToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "Membership_MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Students", "Membership_MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Members", new[] { "Membership_MembershipId" });
            DropIndex("dbo.Students", new[] { "Membership_MembershipId" });
            DropIndex("dbo.Students", new[] { "Teacher_TeacherId" });
            DropColumn("dbo.Members", "MembershipId");
            DropColumn("dbo.Students", "MembershipId");
            DropColumn("dbo.Students", "TeacherId");
            RenameColumn(table: "dbo.Members", name: "Membership_MembershipId", newName: "MembershipId");
            RenameColumn(table: "dbo.Students", name: "Membership_MembershipId", newName: "MembershipId");
            RenameColumn(table: "dbo.Students", name: "Teacher_TeacherId", newName: "TeacherId");
            AlterColumn("dbo.Members", "MembershipId", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "MembershipId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "MembershipId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "MembershipId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "MembershipId");
            CreateIndex("dbo.Students", "MembershipId");
            CreateIndex("dbo.Students", "TeacherId");
            AddForeignKey("dbo.Members", "MembershipId", "dbo.Memberships", "MembershipId", cascadeDelete: true);
            AddForeignKey("dbo.Students", "MembershipId", "dbo.Memberships", "MembershipId", cascadeDelete: true);
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Members", "MembershipId", "dbo.Memberships");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "MembershipId" });
            DropIndex("dbo.Members", new[] { "MembershipId" });
            AlterColumn("dbo.Students", "TeacherId", c => c.Int());
            AlterColumn("dbo.Students", "MembershipId", c => c.Int());
            AlterColumn("dbo.Students", "TeacherId", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "MembershipId", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "MembershipId", c => c.Int());
            AlterColumn("dbo.Members", "MembershipId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Students", name: "TeacherId", newName: "Teacher_TeacherId");
            RenameColumn(table: "dbo.Students", name: "MembershipId", newName: "Membership_MembershipId");
            RenameColumn(table: "dbo.Members", name: "MembershipId", newName: "Membership_MembershipId");
            AddColumn("dbo.Students", "TeacherId", c => c.String(nullable: false));
            AddColumn("dbo.Students", "MembershipId", c => c.String(nullable: false));
            AddColumn("dbo.Members", "MembershipId", c => c.String(nullable: false));
            CreateIndex("dbo.Students", "Teacher_TeacherId");
            CreateIndex("dbo.Students", "Membership_MembershipId");
            CreateIndex("dbo.Members", "Membership_MembershipId");
            AddForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.Students", "Membership_MembershipId", "dbo.Memberships", "MembershipId");
            AddForeignKey("dbo.Members", "Membership_MembershipId", "dbo.Memberships", "MembershipId");
        }
    }
}
