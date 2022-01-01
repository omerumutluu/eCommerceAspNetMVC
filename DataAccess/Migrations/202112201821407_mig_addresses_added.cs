namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addresses_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AddressDetail = c.String(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.Addresses");
        }
    }
}
