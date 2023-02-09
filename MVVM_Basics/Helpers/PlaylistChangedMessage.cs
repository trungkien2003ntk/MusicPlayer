using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Basics.Helpers
{
    public class PlaylistChangedMessage : MessageBase
    {

        public PlaylistChangedMessage(PlaylistPageViewModel changedViewModel)
        {
            ChangedViewModel = changedViewModel;
        }

        public PlaylistPageViewModel ChangedViewModel { get; }
    }
}
