namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_initializer_of_entity_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(maxLength: 60),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CorporateAccountId = c.Int(nullable: true),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 100),
                        ProductThumbnail = c.String(maxLength: 150),
                        UnitPrice = c.Int(),
                        UnitsInStock = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.CorporateAccounts", t => t.CorporateAccountId, cascadeDelete: false)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CorporateAccountId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.CorporateAccounts",
                c => new
                    {
                        CorporateAccountId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CorporateName = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 15),
                        WebSite = c.String(maxLength: 50),
                        Role = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CorporateAccountId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        CustomerAccountId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        Role = c.String(maxLength: 50),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerAccountId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CustomerAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.CorporateAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Products", "CorporateAccountId", "dbo.CorporateAccounts");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.CustomerAccounts", new[] { "UserId" });
            DropIndex("dbo.CorporateAccounts", new[] { "UserId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "CorporateAccountId" });
            DropTable("dbo.Categories");
            DropTable("dbo.CustomerAccounts");
            DropTable("dbo.CorporateAccounts");
            DropTable("dbo.Users");
            DropTable("dbo.Carts");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
