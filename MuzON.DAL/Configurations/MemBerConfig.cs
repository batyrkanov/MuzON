using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    class MemberConfig : EntityTypeConfiguration<Member>
    {
        public MemberConfig()
        {
            HasKey(x => x.Id);

            HasRequired(i => i.Artist)
                .WithMany(i => i.Members)
                .HasForeignKey(i => i.ArtistId)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.Band)
                .WithMany(i => i.Members)
                .HasForeignKey(i => i.BandId)
                .WillCascadeOnDelete(false);
        }
    }
}
