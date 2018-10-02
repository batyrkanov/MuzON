using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    class BandSongConfig : EntityTypeConfiguration<BandSong>
    {
        public BandSongConfig()
        {
            HasKey(e => new { e.ArtistId, e.BandId, e.SongId });

            HasRequired(e => e.Song)
                .WithMany(e => e.BandSongs)
                .HasForeignKey(e => e.SongId);

            HasOptional(e => e.Band)
                .WithMany(e => e.BandSongs)
                .HasForeignKey(e => e.BandId);

            HasOptional(e => e.Artist)
                 .WithMany(e => e.BandSongs)
                 .HasForeignKey(e => e.ArtistId);
        }
    }
}
