namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(maxLength: 128),
                        City = c.String(maxLength: 128),
                        PostalCode = c.String(maxLength: 16),
                        Country = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "cust.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        OrderId = c.Int(),
                        InvoiceDate = c.DateTime(nullable: false),
                        Discount = c.Single(nullable: false),
                        Street = c.String(maxLength: 128),
                        City = c.String(maxLength: 128),
                        PostalCode = c.String(maxLength: 16),
                        Country = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("cust.Customers", t => t.CustomerId)
                .ForeignKey("cust.Orders", t => t.InvoiceId)
                .Index(t => t.InvoiceId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "cust.InvoicePositions",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 40),
                        ProductDescription = c.String(maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Int(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvoiceId, t.Position })
                .ForeignKey("cust.Invoices", t => t.InvoiceId)
                .ForeignKey("cust.Product", t => t.ProductId)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            AddColumn("cust.Customers", "AddressId", c => c.Int());
            AddColumn("cust.Orders", "InvoiceId", c => c.Int());
            CreateIndex("cust.Customers", "AddressId");
            AddForeignKey("cust.Customers", "AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("cust.InvoicePositions", "ProductId", "cust.Product");
            DropForeignKey("cust.InvoicePositions", "InvoiceId", "cust.Invoices");
            DropForeignKey("cust.Invoices", "InvoiceId", "cust.Orders");
            DropForeignKey("cust.Invoices", "CustomerId", "cust.Customers");
            DropForeignKey("cust.Customers", "AddressId", "dbo.Addresses");
            DropIndex("cust.InvoicePositions", new[] { "ProductId" });
            DropIndex("cust.InvoicePositions", new[] { "InvoiceId" });
            DropIndex("cust.Invoices", new[] { "CustomerId" });
            DropIndex("cust.Invoices", new[] { "InvoiceId" });
            DropIndex("cust.Customers", new[] { "AddressId" });
            DropColumn("cust.Orders", "InvoiceId");
            DropColumn("cust.Customers", "AddressId");
            DropTable("cust.InvoicePositions");
            DropTable("cust.Invoices");
            DropTable("dbo.Addresses");
        }
    }
}
