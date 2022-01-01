namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_orders_dependency_changed_to_customerAccountId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "User_UserId");
            AddColumn("dbo.Orders", "CustomerAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "User_UserId", c => c.Int());
            CreateIndex("dbo.Orders", "CustomerAccountId");
            CreateIndex("dbo.Orders", "User_UserId");
            AddForeignKey("dbo.Orders", "CustomerAccountId", "dbo.CustomerAccounts", "CustomerAccountId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "CustomerAccountId", "dbo.CustomerAccounts");
            DropIndex("dbo.Orders", new[] { "User_UserId" });
            DropIndex("dbo.Orders", new[] { "CustomerAccountId" });
            AlterColumn("dbo.Orders", "User_UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "CustomerAccountId");
            RenameColumn(table: "dbo.Orders", name: "User_UserId", newName: "UserId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
