using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        // Configure with FLUENT API
        public ApplicationUserConfig()
        {
            HasMany(p => p.Roles)
            .WithRequired()
            .HasForeignKey(p => p.UserId);
        }
    }
}
