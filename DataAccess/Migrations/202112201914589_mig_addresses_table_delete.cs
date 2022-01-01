namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addresses_table_delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            AddColumn("dbo.CustomerAccounts", "Address", c => c.String());
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AddressDetail = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            DropColumn("dbo.CustomerAccounts", "Address");
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
