using System;
using System.Collections.Generic;

namespace MVVM_Basics.Models
{
    public partial class UserPlay
    {
        public int UsersId { get; set; }
        public int SongId { get; set; }
        public int? Play { get; set; }
        public DateTime? LastestPlayDate { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
