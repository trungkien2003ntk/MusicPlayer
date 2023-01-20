using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Interfaces;
using MVVM_Basics.Models;
using System;
using System.IO;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class SongControlViewModel : ViewModelBase, ISoundPlayer
{
    private readonly IServiceProvider _ServiceProvider;
    private ISharedDataContext _SharedDataContext;


    public ICommand? SkipNextCommand { get; set; }

    private Song _CurrentPlayingSong;
    public Song CurrentPlayingSong
    {
        get { return _CurrentPlayingSong; }
        set { _CurrentPlayingSong = value; _SharedDataContext.CurrentPlayingSong = value; OnPropertyChanged(); }
    }


    private bool _IsPlaying;
    public bool IsPlaying
    {
        get { return _IsPlaying; }
        set
        {
            _IsPlaying = value;
            if (HasASongLoaded())
            {
                if (_IsPlaying)
                    Play!();
                else
                    Pause!();
            }
            OnPropertyChanged();
        }
    }


    public event Action? Play;
    public event Action? Pause;
    public event ISoundPlayer.ChangeSong? OnSongChanged;


    public SongControlViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = serviceProvider.GetRequiredService<ISharedDataContext>();

        _IsPlaying = false;

        _CurrentPlayingSong = new Song();

        if (HasASongLoaded())
        {
            _CurrentPlayingSong = _SharedDataContext.CurrentPlayingSong!;
        }

        Messenger.Default.Register<ChangeSongMessage>(this, OnChangeSongMessageReceived);

        InitializeCommands();
    }

    private void InitializeCommands()
    {
        SkipNextCommand = new RelayCommand<Song>(
            (p) => { if (_SharedDataContext.SongQueue.Count > 0) return true; return false; },
            (p) =>
            {
                var nextSong = _SharedDataContext.SongQueue[0];

                SkipSong(nextSong);

                _SharedDataContext.SongQueue.RemoveAt(0);
            });
    }

    private bool HasASongLoaded()
    {
        return _SharedDataContext.CurrentPlayingSong != null;
    }

    private void OnChangeSongMessageReceived(ChangeSongMessage message)
    {
        var songToPlay = message.Song;

        if (songToPlay != null)
        {
            SkipSong(songToPlay);
        }
        else
        {
            // Show message song not available
        }
    }

    private void SkipSong(Song songToPlay)
    {
        if (File.Exists(songToPlay.PcLink))
        {
            if (songToPlay.Id != CurrentPlayingSong.Id)
            {
                CurrentPlayingSong = songToPlay;
            }

            OnSongChanged?.Invoke(CurrentPlayingSong.PcLink!);
            IsPlaying = true;
        }
        else
        {
            // Download song from blob storage
        }

        
    }
}
