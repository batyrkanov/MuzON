using Microsoft.AspNet.Identity;
using System;

namespace MuzON.Domain.Identity
{
    public class ApplicationRoleManager : RoleManager<Role, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Role, Guid> roleStore)
            : base(roleStore)
        { }
    }
}
