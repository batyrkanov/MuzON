namespace MuzON.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileNameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "FileName");
        }
    }
}
