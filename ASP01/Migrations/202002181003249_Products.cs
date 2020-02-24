namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cust.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("cust.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "cust.OrderPositions",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.Position })
                .ForeignKey("cust.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("cust.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "cust.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                        Description = c.String(maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("cust.OrderPositions", "ProductId", "cust.Product");
            DropForeignKey("cust.OrderPositions", "OrderId", "cust.Orders");
            DropForeignKey("cust.Orders", "CustomerId", "cust.Customers");
            DropIndex("cust.OrderPositions", new[] { "ProductId" });
            DropIndex("cust.OrderPositions", new[] { "OrderId" });
            DropIndex("cust.Orders", new[] { "CustomerId" });
            DropTable("cust.Product");
            DropTable("cust.OrderPositions");
            DropTable("cust.Orders");
        }
    }
}
