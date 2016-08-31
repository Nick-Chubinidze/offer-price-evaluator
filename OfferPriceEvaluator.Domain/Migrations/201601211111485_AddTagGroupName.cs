namespace OfferPriceEvaluator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagGroupName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagGroups", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TagGroups", "Name");
        }
    }
}
