namespace DiscussionBoard.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration20151014 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TopicName = c.String(),
                    })
                .PrimaryKey(t => t.TopicId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentDetail = c.String(),
                        MemberId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        InterestId = c.Int(nullable: false, identity: true),
                        InterestName = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InterestId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        MemberId = c.Int(nullable: false),
                        MobilePhone = c.String(),
                        TwitterAlias = c.String(),
                        Facebook = c.String(),
                        LinkedIn = c.String(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.TopicCategories",
                c => new
                    {
                        Topic_TopicId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Topic_TopicId, t.Category_CategoryId })
                .ForeignKey("dbo.Topics", t => t.Topic_TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Topic_TopicId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interests", "MemberId", "dbo.Members");
            DropForeignKey("dbo.ContactDetails", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Comments", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.TopicCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TopicCategories", "Topic_TopicId", "dbo.Topics");
            DropIndex("dbo.TopicCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.TopicCategories", new[] { "Topic_TopicId" });
            DropIndex("dbo.ContactDetails", new[] { "MemberId" });
            DropIndex("dbo.Interests", new[] { "MemberId" });
            DropIndex("dbo.Comments", new[] { "TopicId" });
            DropTable("dbo.TopicCategories");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Members");
            DropTable("dbo.Interests");
            DropTable("dbo.Comments");
            DropTable("dbo.Topics");
            DropTable("dbo.Categories");
        }
    }
}
