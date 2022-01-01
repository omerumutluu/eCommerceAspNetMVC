namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_admin_table_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminAccounts",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Role = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.AdminAccounts", new[] { "UserId" });
            DropTable("dbo.AdminAccounts");
        }
    }
}
