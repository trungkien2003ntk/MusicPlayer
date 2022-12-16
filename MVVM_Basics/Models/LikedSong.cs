using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class LikedSong
{
    public int UsersId { get; set; }

    public int SongId { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
