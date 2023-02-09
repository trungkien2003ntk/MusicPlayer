using GalaSoft.MvvmLight.Messaging;
using System;

namespace MVVM_Basics.Helpers;

public class SongAlreadyInPlaylistMessage : MessageBase
{
    public string PlaylistName { get; set; }
    public Action<bool?> Callback { get; set; }

    public SongAlreadyInPlaylistMessage(string playlistName, Action<bool?> callback)
    {
        PlaylistName = playlistName;
        Callback = callback;
    }
}
