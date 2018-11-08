namespace WebAppEFMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductNameAllowNulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
