namespace MuzON.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedreferencetoplaylistincomments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "SongId", "dbo.Songs");
            DropIndex("dbo.Comments", new[] { "SongId" });
            AddColumn("dbo.Comments", "PlaylistId", c => c.Guid());
            AlterColumn("dbo.Comments", "SongId", c => c.Guid());
            CreateIndex("dbo.Comments", "SongId");
            CreateIndex("dbo.Comments", "PlaylistId");
            AddForeignKey("dbo.Comments", "PlaylistId", "dbo.Playlists", "Id");
            AddForeignKey("dbo.Comments", "SongId", "dbo.Songs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Comments", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.Comments", new[] { "PlaylistId" });
            DropIndex("dbo.Comments", new[] { "SongId" });
            AlterColumn("dbo.Comments", "SongId", c => c.Guid(nullable: false));
            DropColumn("dbo.Comments", "PlaylistId");
            CreateIndex("dbo.Comments", "SongId");
            AddForeignKey("dbo.Comments", "SongId", "dbo.Songs", "Id", cascadeDelete: true);
        }
    }
}
