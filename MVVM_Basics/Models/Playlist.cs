using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models;

public partial class Playlist
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UsersId { get; set; }

    public virtual ICollection<PlaylistSong> PlaylistSongs { get; } = new List<PlaylistSong>();

    public virtual User? Users { get; set; }
}
