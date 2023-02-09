using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class Artist
    {
        public Artist()
        {
            AlbumArtists = new HashSet<AlbumArtist>();
            FollowingArtists = new HashSet<FollowingArtist>();
            SongArtists = new HashSet<SongArtist>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AlbumArtist> AlbumArtists { get; set; }
        public virtual ICollection<FollowingArtist> FollowingArtists { get; set; }
        public virtual ICollection<SongArtist> SongArtists { get; set; }
    }
}
