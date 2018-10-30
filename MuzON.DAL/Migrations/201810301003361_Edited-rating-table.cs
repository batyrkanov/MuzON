namespace MuzON.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editedratingtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "SongId", "dbo.Songs");
            DropIndex("dbo.Ratings", new[] { "SongId" });
            AddColumn("dbo.Ratings", "PlaylistId", c => c.Guid());
            AlterColumn("dbo.Ratings", "SongId", c => c.Guid());
            CreateIndex("dbo.Ratings", "SongId");
            CreateIndex("dbo.Ratings", "PlaylistId");
            AddForeignKey("dbo.Ratings", "PlaylistId", "dbo.Playlists", "Id");
            AddForeignKey("dbo.Ratings", "SongId", "dbo.Songs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Ratings", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.Ratings", new[] { "PlaylistId" });
            DropIndex("dbo.Ratings", new[] { "SongId" });
            AlterColumn("dbo.Ratings", "SongId", c => c.Guid(nullable: false));
            DropColumn("dbo.Ratings", "PlaylistId");
            CreateIndex("dbo.Ratings", "SongId");
            AddForeignKey("dbo.Ratings", "SongId", "dbo.Songs", "Id", cascadeDelete: true);
        }
    }
}
