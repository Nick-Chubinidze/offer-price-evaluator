namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyRowToAlternativePriceItemTagTableAndChangePriceType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlternativePriceItemTags", "Currency", c => c.String());
            AlterColumn("dbo.AlternativePriceItemTags", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlternativePriceItemTags", "Price", c => c.String());
            DropColumn("dbo.AlternativePriceItemTags", "Currency");
        }
    }
}
