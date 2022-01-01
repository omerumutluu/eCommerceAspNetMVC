namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchecking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "User_UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "User_UserId" });
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "User_UserId", c => c.Int());
            CreateIndex("dbo.Orders", "User_UserId");
            AddForeignKey("dbo.Orders", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
