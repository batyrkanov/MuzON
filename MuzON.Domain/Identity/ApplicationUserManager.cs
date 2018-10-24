using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MuzON.Domain.Identity
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        public ApplicationUserManager(IUserStore<User, Guid> store)
            : base(store)
        {
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("ASP.NET IDENTITY");
            this.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(provider.Create("Confirmation"))
            {
                TokenLifespan = TimeSpan.FromHours(24),
            };
        }
    }
}
