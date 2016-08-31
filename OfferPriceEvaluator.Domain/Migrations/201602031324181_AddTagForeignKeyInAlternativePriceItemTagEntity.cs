namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagForeignKeyInAlternativePriceItemTagEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlternativePriceItemTags", "Tag_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AlternativePriceItemTags", "Tag_Id");
            AddForeignKey("dbo.AlternativePriceItemTags", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlternativePriceItemTags", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.AlternativePriceItemTags", new[] { "Tag_Id" });
            DropColumn("dbo.AlternativePriceItemTags", "Tag_Id");
        }
    }
}
