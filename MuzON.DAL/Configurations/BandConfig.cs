using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    class BandConfig : EntityTypeConfiguration<Band>
    {
        public BandConfig()
        {
            HasMany(c => c.Artists)
                .WithMany(s => s.Bands)
                .Map(t => t.MapLeftKey("ArtistId")
                .MapRightKey("BandId")
                .ToTable("Members"));
        }
    }
}
