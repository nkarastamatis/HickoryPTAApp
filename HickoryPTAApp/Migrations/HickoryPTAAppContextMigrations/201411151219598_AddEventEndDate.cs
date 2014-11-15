namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventEndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "EndDate");
        }
    }
}
