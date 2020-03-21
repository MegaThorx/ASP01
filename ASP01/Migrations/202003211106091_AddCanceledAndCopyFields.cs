namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCanceledAndCopyFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("cust.Invoices", "Copy", c => c.Boolean(nullable: false));
            AddColumn("cust.Invoices", "Canceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("cust.Invoices", "Canceled");
            DropColumn("cust.Invoices", "Copy");
        }
    }
}
