using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzON.Domain.Identity.IdentityModels;

namespace MuzON.DAL.Identity
{
    // Extend Identity classes to specify a Guid for the key
    public class CustomRoleStore : RoleStore<CustomRole, Guid, CustomUserRole>
    {
        public CustomRoleStore(MuzONContext context)
            : base(context)
        { }
    }
}
