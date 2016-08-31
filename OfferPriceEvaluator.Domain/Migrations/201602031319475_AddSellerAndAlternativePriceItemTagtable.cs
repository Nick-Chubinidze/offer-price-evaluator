namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSellerAndAlternativePriceItemTagtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlternativePriceItemTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.String(),
                        Link = c.String(),
                        Item_Id = c.Int(nullable: false),
                        Seller_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlternativePriceItemTags", "Seller_Id", "dbo.Sellers");
            DropForeignKey("dbo.AlternativePriceItemTags", "Item_Id", "dbo.Items");
            DropIndex("dbo.AlternativePriceItemTags", new[] { "Seller_Id" });
            DropIndex("dbo.AlternativePriceItemTags", new[] { "Item_Id" });
            DropTable("dbo.Sellers");
            DropTable("dbo.AlternativePriceItemTags");
        }
    }
}
