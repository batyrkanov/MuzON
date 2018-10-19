
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MuzON.DAL.EF;
using MuzON.DAL.Identity;
using MuzON.Domain.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MuzON.Web.Startup))]
namespace MuzON.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                
            });
            //createRolesandUsers();

        }

        //private void createRolesandUsers()
        //{
        //    MuzONContext context = new MuzONContext("DefaultConnection");
        //    var roleStore = new RoleStore(context);
        //    var userStore = new UserStore(context);
        //    var roleManager = new ApplicationRoleManager(roleStore);
        //    var UserManager = new ApplicationUserManager(userStore);


        //    if (!context.Roles.Any(r => r.Name == "admin"))
        //    {
        //        var role = new Role { Id = Guid.NewGuid(), Name = "admin" };
        //        roleManager.Create(role);

        //        var user = new User();
        //        user.Id = Guid.NewGuid();
        //        user.Email = "admin@admin.com";
        //        user.UserName = user.Email;

        //        string userPWD = "123123";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "admin");

        //        }
        //    }
        //}
    }
}