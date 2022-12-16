using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class AlbumSong
{
    public int SongId { get; set; }

    public int AlbumId { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
