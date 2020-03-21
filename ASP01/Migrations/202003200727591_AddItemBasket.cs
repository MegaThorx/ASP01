namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cust.BasketItem",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProductId })
                .ForeignKey("cust.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("cust.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.Customer_CustomerId);
            
            AddColumn("dbo.AspNetUsers", "CustomerId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CustomerId");
            AddForeignKey("dbo.AspNetUsers", "CustomerId", "cust.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("cust.BasketItem", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CustomerId", "cust.Customers");
            DropForeignKey("cust.BasketItem", "Customer_CustomerId", "cust.Customers");
            DropForeignKey("cust.BasketItem", "ProductId", "cust.Product");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerId" });
            DropIndex("cust.BasketItem", new[] { "Customer_CustomerId" });
            DropIndex("cust.BasketItem", new[] { "ProductId" });
            DropIndex("cust.BasketItem", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "CustomerId");
            DropTable("cust.BasketItem");
        }
    }
}
