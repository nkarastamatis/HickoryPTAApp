namespace PTAData.Migrations.MembershipContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memberships", "Type");
        }
    }
}
