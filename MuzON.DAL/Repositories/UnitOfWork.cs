using Microsoft.AspNet.Identity.Owin;
using MuzON.DAL.EF;
using MuzON.DAL.Identity;
using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using MuzON.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace MuzON.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseRepository<Artist> artistRepository;
        private BaseRepository<Band> bandRepository;
        private BaseRepository<Comment> commentRepository;
        private BaseRepository<Country> countryRepository;
        private BaseRepository<Genre> genreRepository;
        private BaseRepository<Playlist> playlistRepository;
        private BaseRepository<Rating> ratingRepository;
        private BaseRepository<Song> songRepository;
        private BaseRepository<User> userRepository;
        private BaseRepository<Role> roleRepository;
        private MuzONContext context;

        public UnitOfWork(string connectionString)
        {
            context = new MuzONContext(connectionString);
            context.Database.Log = Log;
        }

        public void Log(string Message)
        {
            Console.WriteLine(Message);
        }

        public IRepository<Artist> Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new BaseRepository<Artist>(context);
                return artistRepository;
            }
        }

        public IRepository<Band> Bands
        {
            get
            {
                if (bandRepository == null)
                    bandRepository = new BaseRepository<Band>(context);
                return bandRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new BaseRepository<Comment>(context);
                return commentRepository;
            }
        }

        public IRepository<Rating> Ratings
        {
            get
            {
                if (ratingRepository == null)
                    ratingRepository = new BaseRepository<Rating>(context);
                return ratingRepository;
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                if (countryRepository == null)
                    countryRepository = new BaseRepository<Country>(context);
                return countryRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new BaseRepository<Genre>(context);
                return genreRepository;
            }
        }

        public IRepository<Playlist> Playlists
        {
            get
            {
                if (playlistRepository == null)
                    playlistRepository = new BaseRepository<Playlist>(context);
                return playlistRepository;
            }
        }

        public IRepository<Song> Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new BaseRepository<Song>(context);
                return songRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new BaseRepository<User>(context);
                return userRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new BaseRepository<Role>(context);
                return roleRepository;
            }
        }

        // Save
        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        // Dispose
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
