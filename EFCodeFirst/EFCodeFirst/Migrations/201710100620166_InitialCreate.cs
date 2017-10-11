namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Cnum = c.Int(nullable: false, identity: true),
                        Cname = c.String(),
                        City = c.String(),
                        Rating = c.Int(nullable: false),
                        Salesperson_Snum = c.Int(),
                    })
                .PrimaryKey(t => t.Cnum)
                .ForeignKey("dbo.Salespersons", t => t.Salesperson_Snum)
                .Index(t => t.Salesperson_Snum);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Onum = c.Int(nullable: false, identity: true),
                        Odate = c.DateTime(nullable: false),
                        Amt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Cnum = c.Int(),
                        Salesperson_Snum = c.Int(),
                    })
                .PrimaryKey(t => t.Onum)
                .ForeignKey("dbo.Customers", t => t.Customer_Cnum)
                .ForeignKey("dbo.Salespersons", t => t.Salesperson_Snum)
                .Index(t => t.Customer_Cnum)
                .Index(t => t.Salesperson_Snum);
            
            CreateTable(
                "dbo.Salespersons",
                c => new
                    {
                        Snum = c.Int(nullable: false, identity: true),
                        Sname = c.String(),
                        City = c.String(),
                        Comm = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Snum);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Salesperson_Snum", "dbo.Salespersons");
            DropForeignKey("dbo.Orders", "Salesperson_Snum", "dbo.Salespersons");
            DropForeignKey("dbo.Orders", "Customer_Cnum", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Salesperson_Snum" });
            DropIndex("dbo.Orders", new[] { "Customer_Cnum" });
            DropIndex("dbo.Customers", new[] { "Salesperson_Snum" });
            DropTable("dbo.Salespersons");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
