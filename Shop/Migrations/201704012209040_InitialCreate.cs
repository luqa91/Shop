namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(nullable: false, maxLength: 100),
                        DescriptionCategory = c.String(nullable: false),
                        NameFileIcon = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Company = c.String(nullable: false, maxLength: 100),
                        DateAdded = c.DateTime(nullable: false),
                        NameFileImage = c.String(maxLength: 100),
                        Description = c.String(),
                        PriceProduct = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bestseller = c.Boolean(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        ShortDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        PostalCode = c.String(nullable: false, maxLength: 6),
                        Phone = c.String(),
                        Email = c.String(),
                        Comment = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        StatusOrder = c.Int(nullable: false),
                        ValueOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.PositionOrder",
                c => new
                    {
                        PositionOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PricePurchase = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PositionOrderId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionOrder", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PositionOrder", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.PositionOrder", new[] { "ProductId" });
            DropIndex("dbo.PositionOrder", new[] { "OrderId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropTable("dbo.PositionOrder");
            DropTable("dbo.Order");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
