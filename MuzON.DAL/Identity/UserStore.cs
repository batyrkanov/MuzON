using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.DAL.EF;
using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Identity
{
    // Extend Identity classes to specify a Guid for the key
    public class UserStore : UserStore<ApplicationUser, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public UserStore(MuzONContext context)
            : base(context)
        { }
    }
}
