using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.ObjectModel;

namespace MVVM_Basics.ViewModels;

public class QueuePageViewModel : ViewModelBase
{
    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;


	private Song? _CurrentPlayingSong;
    public Song? CurrentPlayingSong
    {
		get { return _CurrentPlayingSong; }
		set { _CurrentPlayingSong = value; OnPropertyChanged(); }
	}


    private ObservableCollection<Song> _SongQueue;

    public ObservableCollection<Song> SongQueue
    {
        get { return _SongQueue; }
        set { _SongQueue = value; OnPropertyChanged(); }
    }


    public QueuePageViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = serviceProvider.GetRequiredService<ISharedDataContext>();
        _CurrentPlayingSong = _SharedDataContext.CurrentPlayingSong;
        _SongQueue = _SharedDataContext.SongQueue;


        Messenger.Default.Register<ChangeSongMessage>(this, OnChangeSongMessageReceived);
    }

    private void OnChangeSongMessageReceived(ChangeSongMessage message)
    {
        var songToChange = message.Song;

        if (songToChange != null)
        {
            CurrentPlayingSong = songToChange;
        }
        else
        {
            // Show noti Song not avaiable
        }
    }

    private void HandleAddSongToQueueMessage(AddSongToQueueMessage message)
    {
        var songToAdd = message.Song;

        if (!SongQueue.Contains(songToAdd) && songToAdd != null) 
        {
            SongQueue.Add(songToAdd);

            // play the song if it is the first song to be added to the queue
            if (SongQueue.Count == 1)
            {
                Messenger.Default.Send(new ChangeSongMessage(songToAdd));
            }
        }

    }
}
