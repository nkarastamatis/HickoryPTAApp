namespace HickoryPTAApp.Migrations.HickoryPTAAppContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePostKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" }, "dbo.CommitteePosts");
            DropForeignKey("dbo.PostFiles", "CommitteePost_PostId", "dbo.CommitteePosts");
            DropIndex("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" });
            DropPrimaryKey("dbo.CommitteePosts");
            AlterColumn("dbo.CommitteePosts", "PostId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CommitteePosts", "PostId");
            CreateIndex("dbo.PostFiles", "CommitteePost_PostId");
            AddForeignKey("dbo.PostFiles", "CommitteePost_PostId", "dbo.CommitteePosts", "PostId");
            DropColumn("dbo.PostFiles", "CommitteePost_CommitteeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostFiles", "CommitteePost_CommitteeId", c => c.Int());
            DropForeignKey("dbo.PostFiles", "CommitteePost_PostId", "dbo.CommitteePosts");
            DropIndex("dbo.PostFiles", new[] { "CommitteePost_PostId" });
            DropPrimaryKey("dbo.CommitteePosts");
            AlterColumn("dbo.CommitteePosts", "PostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CommitteePosts", new[] { "PostId", "CommitteeId" });
            CreateIndex("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" });
            AddForeignKey("dbo.PostFiles", "CommitteePost_PostId", "dbo.CommitteePosts", "PostId");
            AddForeignKey("dbo.PostFiles", new[] { "CommitteePost_PostId", "CommitteePost_CommitteeId" }, "dbo.CommitteePosts", new[] { "PostId", "CommitteeId" });
        }
    }
}
