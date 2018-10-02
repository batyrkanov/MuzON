using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Configurations
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        // Configure with FLUENT API
        public UserConfig()
        {
            HasMany(p => p.Roles)
            .WithRequired()
            .HasForeignKey(p => p.UserId);
        }
    }
}
