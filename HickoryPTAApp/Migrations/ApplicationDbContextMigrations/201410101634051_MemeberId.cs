namespace HickoryPTAApp.Migrations.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemeberId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "MemberId", "dbo.Members");
            DropIndex("dbo.AspNetUsers", new[] { "MemberId" });
            AlterColumn("dbo.AspNetUsers", "MemberId", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "MemberId");
            AddForeignKey("dbo.AspNetUsers", "MemberId", "dbo.Members", "MemberId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "MemberId", "dbo.Members");
            DropIndex("dbo.AspNetUsers", new[] { "MemberId" });
            AlterColumn("dbo.AspNetUsers", "MemberId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Members", "MemberId");
            CreateIndex("dbo.AspNetUsers", "MemberId");
            AddForeignKey("dbo.AspNetUsers", "MemberId", "dbo.Members", "MemberId");
        }
    }
}
