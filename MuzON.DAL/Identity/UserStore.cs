using MuzON.DAL.EF;
using MuzON.Domain.Identity;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MuzON.DAL.Identity
{
    // Extend Identity classes to specify a Guid for the key
    public class UserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public UserStore(MuzONContext context)
            : base(context)
        { }
    }
}
