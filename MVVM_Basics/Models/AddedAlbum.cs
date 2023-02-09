using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class AddedAlbum
    {
        public int UsersId { get; set; }
        public int AlbumId { get; set; }
        public DateTime? AddedDate { get; set; }

        public virtual Album Album { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
