using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class Song
    {
        public Song()
        {
            AlbumSongs = new HashSet<AlbumSong>();
            LikedSongs = new HashSet<LikedSong>();
            PlaylistSongs = new HashSet<PlaylistSong>();
            SongArtists = new HashSet<SongArtist>();
            UserPlays = new HashSet<UserPlay>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Plays { get; set; }
        public DateTime? LastestPlayDate { get; set; }
        public string? StorageLink { get; set; }
        public string? PcLink { get; set; }

        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }
        public virtual ICollection<LikedSong> LikedSongs { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
        public virtual ICollection<SongArtist> SongArtists { get; set; }
        public virtual ICollection<UserPlay> UserPlays { get; set; }
    }
}
