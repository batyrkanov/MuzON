using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MuzON.DAL.EF;
using MuzON.DAL.Identity;
using MuzON.Domain.Identity;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.App_Start
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        public ApplicationUserManager(IUserStore<User, Guid> store)
            : base(store)
        { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            MuzONContext db = context.Get<MuzONContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore(db));
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<Role, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Role, Guid> roleStore)
            : base(roleStore)
        { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            MuzONContext db = context.Get<MuzONContext>();
            ApplicationRoleManager manager = new ApplicationRoleManager(new RoleStore(db));
            
            return manager;
        }
    }
}
