using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class SongArtist
    {
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public string? AdditionalInfo { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual Song Song { get; set; } = null!;
    }
}
