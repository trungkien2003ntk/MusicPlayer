using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class FollowingArtist
{
    public int UsersId { get; set; }

    public int ArtistId { get; set; }

    public DateTime? FollowDate { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
