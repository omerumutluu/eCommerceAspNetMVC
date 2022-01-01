namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_product_int_nullable_false : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "UnitPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "UnitsInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "UnitsInStock", c => c.Int());
            AlterColumn("dbo.Products", "UnitPrice", c => c.Int());
        }
    }
}
