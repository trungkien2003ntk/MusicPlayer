using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public DateTime? JoinDate { get; set; }

    public virtual ICollection<AddedAlbum> AddedAlbums { get; } = new List<AddedAlbum>();

    public virtual ICollection<FollowingArtist> FollowingArtists { get; } = new List<FollowingArtist>();

    public virtual ICollection<LikedSong> LikedSongs { get; } = new List<LikedSong>();

    public virtual ICollection<Playlist> Playlists { get; } = new List<Playlist>();
}
