namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cust.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Content = c.String(maxLength: 200),
                        Parent_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("cust.Comments", t => t.Parent_CommentId)
                .ForeignKey("cust.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.Parent_CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("cust.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("cust.Comments", "ProductId", "cust.Product");
            DropForeignKey("cust.Comments", "Parent_CommentId", "cust.Comments");
            DropIndex("cust.Comments", new[] { "Parent_CommentId" });
            DropIndex("cust.Comments", new[] { "ProductId" });
            DropIndex("cust.Comments", new[] { "UserId" });
            DropTable("cust.Comments");
        }
    }
}
