using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Identity
{
    public class Role : IdentityRole<Guid, UserRole>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
