using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    class CustomRoleConfig : EntityTypeConfiguration<Role>
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
