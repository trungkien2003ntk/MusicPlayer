using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class PlaylistSong
{
    public int SongId { get; set; }

    public int PlaylistId { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
