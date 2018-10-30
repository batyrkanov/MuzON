namespace MuzON.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingValueChangedFromIntToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Value", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Value", c => c.Int(nullable: false));
        }
    }
}
