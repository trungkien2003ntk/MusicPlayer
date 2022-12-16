using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class Artist
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AlbumArtist> AlbumArtists { get; } = new List<AlbumArtist>();

    public virtual ICollection<FollowingArtist> FollowingArtists { get; } = new List<FollowingArtist>();

    public virtual ICollection<SongArtist> SongArtists { get; } = new List<SongArtist>();
}
