using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class AlbumArtist
    {
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string? AdditionalInfo { get; set; }

        public virtual Album Album { get; set; } = null!;
        public virtual Artist Artist { get; set; } = null!;
    }
}
