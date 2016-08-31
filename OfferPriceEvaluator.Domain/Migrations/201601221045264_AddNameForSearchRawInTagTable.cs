namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameForSearchRawInTagTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "NameForSearch", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "NameForSearch");
        }
    }
}
