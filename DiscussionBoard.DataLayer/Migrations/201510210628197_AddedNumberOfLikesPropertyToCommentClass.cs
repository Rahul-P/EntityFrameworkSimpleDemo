namespace DiscussionBoard.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberOfLikesPropertyToCommentClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "NumberOfLikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "NumberOfLikes");
        }
    }
}
