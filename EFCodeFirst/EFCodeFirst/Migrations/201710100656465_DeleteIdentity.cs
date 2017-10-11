namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Customer_Cnum", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Salesperson_Snum", "dbo.Salespeople");
            DropForeignKey("dbo.Customers", "Salesperson_Snum", "dbo.Salespeople");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Salespeople");
            AlterColumn("dbo.Customers", "Cnum", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "Onum", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Salespeople", "Snum", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "Cnum");
            AddPrimaryKey("dbo.Orders", "Onum");
            AddPrimaryKey("dbo.Salespeople", "Snum");
            AddForeignKey("dbo.Orders", "Customer_Cnum", "dbo.Customers", "Cnum");
            AddForeignKey("dbo.Orders", "Salesperson_Snum", "dbo.Salespeople", "Snum");
            AddForeignKey("dbo.Customers", "Salesperson_Snum", "dbo.Salespeople", "Snum");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Salesperson_Snum", "dbo.Salespeople");
            DropForeignKey("dbo.Orders", "Salesperson_Snum", "dbo.Salespeople");
            DropForeignKey("dbo.Orders", "Customer_Cnum", "dbo.Customers");
            DropPrimaryKey("dbo.Salespeople");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Salespeople", "Snum", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Onum", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Cnum", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Salespeople", "Snum");
            AddPrimaryKey("dbo.Orders", "Onum");
            AddPrimaryKey("dbo.Customers", "Cnum");
            AddForeignKey("dbo.Customers", "Salesperson_Snum", "dbo.Salespeople", "Snum");
            AddForeignKey("dbo.Orders", "Salesperson_Snum", "dbo.Salespeople", "Snum");
            AddForeignKey("dbo.Orders", "Customer_Cnum", "dbo.Customers", "Cnum");
        }
    }
}
