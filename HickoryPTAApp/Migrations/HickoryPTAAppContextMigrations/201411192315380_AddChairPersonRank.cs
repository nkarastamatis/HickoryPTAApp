namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChairPersonRank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChairPersons", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChairPersons", "Rank");
        }
    }
}
