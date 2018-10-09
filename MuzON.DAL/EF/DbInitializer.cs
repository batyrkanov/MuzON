using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.DAL.Identity;
using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using System;
using System.Data.Entity;
using System.Linq;

namespace MuzON.DAL.EF
{
   public class DbInitializer : DropCreateDatabaseAlways<MuzONContext>
    {
        protected override void Seed(MuzONContext db)
        {
            Genre rockGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock" };
            Genre jRockGenre = new Genre { Id = Guid.NewGuid(), Name = "J-Rock" };

            Country country = new Country { Id = Guid.NewGuid(), Name = "Bishkek" };
            Country berlin = new Country { Id = Guid.NewGuid(), Name = "Berlin" };
            
            db.Genres.Add(rockGenre);
            db.Genres.Add(jRockGenre);
            db.Countries.Add(country);
            db.Countries.Add(berlin);
            db.SaveChanges();
        }
    }
}
