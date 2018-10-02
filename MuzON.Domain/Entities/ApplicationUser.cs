using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.Domain.Identity;
using System;

namespace MuzON.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid, UserLogin, UserRole,
    UserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }
}
