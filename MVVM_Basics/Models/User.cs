using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class User
    {
        public User()
        {
            AddedAlbums = new HashSet<AddedAlbum>();
            FollowingArtists = new HashSet<FollowingArtist>();
            LikedSongs = new HashSet<LikedSong>();
            Playlists = new HashSet<Playlist>();
            UserPlays = new HashSet<UserPlay>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime? JoinDate { get; set; }

        public virtual ICollection<AddedAlbum> AddedAlbums { get; set; }
        public virtual ICollection<FollowingArtist> FollowingArtists { get; set; }
        public virtual ICollection<LikedSong> LikedSongs { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<UserPlay> UserPlays { get; set; }
    }
}
