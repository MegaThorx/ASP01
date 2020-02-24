namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cust.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FName = c.String(maxLength: 100),
                        LName = c.String(nullable: false, maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("cust.Customers");
        }
    }
}
