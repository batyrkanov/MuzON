using MuzON.Domain.Entities;
using System;
using System.Data.Entity;

namespace MuzON.DAL.EF
{
    class DbInitializer : DropCreateDatabaseIfModelChanges<MuzONContext>
    {
        protected override void Seed(MuzONContext db)
        {
            Genre rockGenre = new Genre { Id = Guid.NewGuid(), Name = "Rock" };
            Genre jRockGenre = new Genre { Id = Guid.NewGuid(), Name = "J-Rock" };

            db.Genres.Add(rockGenre);
            db.Genres.Add(jRockGenre);
            db.SaveChanges();
        }
    }
}
