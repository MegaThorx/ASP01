namespace ASP01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerNotesField : DbMigration
    {
        public override void Up()
        {
            AddColumn("cust.Customers", "Notes", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("cust.Customers", "Notes");
        }
    }
}
