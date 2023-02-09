using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class Album
    {
        public Album()
        {
            AddedAlbums = new HashSet<AddedAlbum>();
            AlbumArtists = new HashSet<AlbumArtist>();
            AlbumSongs = new HashSet<AlbumSong>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual ICollection<AddedAlbum> AddedAlbums { get; set; }
        public virtual ICollection<AlbumArtist> AlbumArtists { get; set; }
        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }
    }
}
