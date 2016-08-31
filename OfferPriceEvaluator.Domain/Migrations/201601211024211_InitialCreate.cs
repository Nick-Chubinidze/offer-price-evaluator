namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublishedOn = c.DateTime(nullable: false),
                        ExternalId = c.String(),
                        ContactNumbers = c.String(),
                        OfferTitle = c.String(),
                        OfferLocation = c.String(),
                        ProductDescription = c.String(),
                        IsUsed = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Url = c.String(),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ItemTagValues",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.ItemID, t.TagID })
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TagToTagGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagGroupId_id = c.Int(),
                        TagId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagGroups", t => t.TagGroupId_id)
                .ForeignKey("dbo.Tags", t => t.TagId_Id)
                .Index(t => t.TagGroupId_id)
                .Index(t => t.TagId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagToTagGroups", "TagId_Id", "dbo.Tags");
            DropForeignKey("dbo.TagToTagGroups", "TagGroupId_id", "dbo.TagGroups");
            DropForeignKey("dbo.ItemTagValues", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ItemTagValues", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropIndex("dbo.TagToTagGroups", new[] { "TagId_Id" });
            DropIndex("dbo.TagToTagGroups", new[] { "TagGroupId_id" });
            DropIndex("dbo.ItemTagValues", new[] { "TagID" });
            DropIndex("dbo.ItemTagValues", new[] { "ItemID" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropTable("dbo.TagToTagGroups");
            DropTable("dbo.TagGroups");
            DropTable("dbo.Tags");
            DropTable("dbo.ItemTagValues");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
