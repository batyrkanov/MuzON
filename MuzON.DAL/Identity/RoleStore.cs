using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.DAL.EF;
using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Identity
{
    // Extend Identity classes to specify a Guid for the key
    public class RoleStore : RoleStore<Role, Guid, UserRole>
    {
        public RoleStore(MuzONContext context)
            : base(context)
        { }
    }
}
