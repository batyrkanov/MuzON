namespace MuzON.DAL.Migrations
{
    using MuzON.DAL.Identity;
    using MuzON.Domain.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MuzON.DAL.EF.MuzONContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(MuzON.DAL.EF.MuzONContext context)
        {
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore(context);
                var manager = new ApplicationRoleManager(store);
                var role = new Role { Id = Guid.NewGuid(), Name = "admin" };

                await manager.CreateAsync(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var store = new UserStore(context);
                var manager = new ApplicationUserManager(store);
                var user = new User { Id = Guid.NewGuid(),
                    Email = "admin@admin.com", UserName = "admin@admin.com" };

                await manager.CreateAsync(user, "123123");
                await manager.AddToRoleAsync(user.Id, "admin");
            }
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
