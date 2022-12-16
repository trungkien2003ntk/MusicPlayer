using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class Album
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public virtual ICollection<AddedAlbum> AddedAlbums { get; } = new List<AddedAlbum>();

    public virtual ICollection<AlbumArtist> AlbumArtists { get; } = new List<AlbumArtist>();

    public virtual ICollection<AlbumSong> AlbumSongs { get; } = new List<AlbumSong>();
}
