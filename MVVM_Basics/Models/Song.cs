using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class Song
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Plays { get; set; }

    public DateTime? LastestPlayDate { get; set; }

    public string? StorageLink { get; set; }

    public string? PcLink { get; set; }

    public virtual ICollection<AlbumSong> AlbumSongs { get; } = new List<AlbumSong>();

    public virtual ICollection<LikedSong> LikedSongs { get; } = new List<LikedSong>();

    public virtual ICollection<PlaylistSong> PlaylistSongs { get; } = new List<PlaylistSong>();

    public virtual ICollection<SongArtist> SongArtists { get; } = new List<SongArtist>();
}
