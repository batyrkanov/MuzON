using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    class SongConfig : EntityTypeConfiguration<Song>
    {
        public SongConfig()
        {
            HasMany(c => c.Genres)
                .WithMany(s => s.Songs)
                .Map(t => t.MapLeftKey("SongId") 
                .MapRightKey("GenreId") 
                .ToTable("SongGenres"));
        }
    }
}
