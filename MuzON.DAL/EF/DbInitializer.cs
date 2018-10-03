using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.DAL.Identity;
using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using System;
using System.Data.Entity;

namespace MuzON.DAL.EF
{
   public class DbInitializer : DropCreateDatabaseIfModelChanges<MuzONContext>
    {
        protected override void Seed(MuzONContext db)
        {
            Genre rockGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock" };
            Genre jRockGenre = new Genre { Id = Guid.NewGuid(), Name = "J-Rock" };

            Country country = new Country { Id = Guid.NewGuid(), Name = "Bishkek" };

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore(db));
            var user = new User {Id = Guid.NewGuid(), Email = "admin@admin.com", UserName = "admin@admin.com" };
            userManager.Create(user, "123123");
            var roleManager = new ApplicationRoleManager(new RoleStore(db));
            roleManager.Create(new Role("admin"));
            userManager.AddToRole(user.Id, "admin");
            db.Genres.Add(rockGenre);
            db.Genres.Add(jRockGenre);
            db.Countries.Add(country);
            db.SaveChanges();
        }
    }
}
