namespace MuzON.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BandAndArtistModelsEdited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Members", "BandId", "dbo.Bands");
            CreateTable(
                "dbo.BandArtists",
                c => new
                    {
                        Band_Id = c.Guid(nullable: false),
                        Artist_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Band_Id, t.Artist_Id })
                .ForeignKey("dbo.Bands", t => t.Band_Id, cascadeDelete: false)
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: false)
                .Index(t => t.Band_Id)
                .Index(t => t.Artist_Id);
            
            AddForeignKey("dbo.Members", "ArtistId", "dbo.Artists", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Members", "BandId", "dbo.Bands", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "BandId", "dbo.Bands");
            DropForeignKey("dbo.Members", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.BandArtists", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.BandArtists", "Band_Id", "dbo.Bands");
            DropIndex("dbo.BandArtists", new[] { "Artist_Id" });
            DropIndex("dbo.BandArtists", new[] { "Band_Id" });
            DropTable("dbo.BandArtists");
            AddForeignKey("dbo.Members", "BandId", "dbo.Bands", "Id");
            AddForeignKey("dbo.Members", "ArtistId", "dbo.Artists", "Id");
        }
    }
}
