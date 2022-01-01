namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_comment_productId_column_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ProductId");
            AddForeignKey("dbo.Comments", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropColumn("dbo.Comments", "ProductId");
        }
    }
}
