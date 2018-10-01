using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzON.Domain.Identity.IdentityModels;

namespace MuzON.DAL.Configurations
{
    class CustomRoleConfig : EntityTypeConfiguration<CustomRole>
    {
        // Configure with FLUENT API
        public CustomRoleConfig()
        {
            HasMany(p => p.Users)
            .WithRequired()
            .HasForeignKey(p => p.RoleId);
        }
    }
}
