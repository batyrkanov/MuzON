using System;
using System.Collections;
using System.Collections.Generic;

namespace MuzON.Domain.Entities
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BandSong> BandSongs { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }

        public Song()
        {
            Playlists = new List<Playlist>();
            BandSongs = new List<BandSong>();
            Comments = new List<Comment>();
            Genres = new List<Genre>();
            Ratings = new List<Rating>();
        }
    }
}
