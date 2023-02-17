using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Interfaces;
using MVVM_Basics.Models;
using MVVM_Basics.Views.MessageAndNotification;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class SongControlViewModel : ViewModelBase, ISoundPlayer
{
    private readonly IServiceProvider _ServiceProvider;
    private ISharedDataContext _SharedDataContext;


    public ICommand? SkipNextCommand { get; set; }

    private Song? _CurrentPlayingSong;
    public Song? CurrentPlayingSong
    {
        get { return _CurrentPlayingSong; }
        set { _CurrentPlayingSong = value; _SharedDataContext.CurrentPlayingSong = value; OnPropertyChanged(); }
    }


    private bool _IsPlaying = false;
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

        if (HasASongLoaded())
        {
            _CurrentPlayingSong = _SharedDataContext.CurrentPlayingSong!;
        }

        Messenger.Default.Register<ChangeSongMessage>(this, OnChangeSongMessageReceived);
        Messenger.Default.Register<IncreasePlayCountMessage>(this, OnIncreasePlayCountMessageReceived);
        Messenger.Default.Register<SaveSongPlayDateMessage>(this, OnSaveSongPlayDateMessageReceived);
        InitializeCommands();
    }

    private void OnSaveSongPlayDateMessageReceived(SaveSongPlayDateMessage message)
    {
        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            CurrentPlayingSong!.LastestPlayDate = DateTime.Now;
            context.Songs.FirstOrDefault(s => s.Id == CurrentPlayingSong.Id)!.LastestPlayDate = CurrentPlayingSong!.LastestPlayDate;
            context.SaveChanges();
        }
    }

    private void OnIncreasePlayCountMessageReceived(IncreasePlayCountMessage message)
    {
        CurrentPlayingSong!.Plays += 1;

        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            Song? currentSongInDB = context.Songs.FirstOrDefault(s => s.Id == CurrentPlayingSong.Id);

            if (currentSongInDB != null)
            {
                currentSongInDB.Plays += 1;
                context.SaveChanges();
            }
        }
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
            // Show noti song not available
        }
    }

    private void InitializeCommands()
    {
        SkipNextCommand = new RelayCommand<Song>(
            (p) => { if (_SharedDataContext.SongQueue.Count > 0) return true; return false; },
            (p) =>
            {
                if (_SharedDataContext.SongQueue.Count <= 0)
                    return;

                var nextSong = _SharedDataContext.SongQueue[0];
                _SharedDataContext.CurrentPlayingSong = nextSong;

                Messenger.Default.Unregister<ChangeSongMessage>(this, OnChangeSongMessageReceived);
                Messenger.Default.Send(new ChangeSongMessage(nextSong));
                Messenger.Default.Register<ChangeSongMessage>(this, OnChangeSongMessageReceived);

                SkipSong(nextSong);

                _SharedDataContext.SongQueue.RemoveAt(0);
            });

        ToggleLikedSongCommand = new RelayCommand<bool> (
            (isLiked) => { return true; },
            (isLiked) => 
            {
                if (!isLiked)
                {
                    using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                    {
                        LikedSong likedSong = new LikedSong()
                        {
                            UsersId = _SharedDataContext.LoginedUserId,
                            SongId = CurrentPlayingSong.Id
                        };

                        context.LikedSongs.Remove(likedSong);
                        context.SaveChanges();


                        // Notify the user that the song was removed from the LikedSongs playlist
                        Messenger.Default.Send(new ShowNotificationMessage(NotificationType.Removed, TargetType.LikedSongs));
                    }
                }
                else
                {
                    using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                    {
                        LikedSong likedSong = new LikedSong()
                        {
                            UsersId = _SharedDataContext.LoginedUserId,
                            SongId = CurrentPlayingSong.Id
                        };

                        context.LikedSongs.Add(likedSong);
                        context.SaveChanges();


                        // Notify the user that the song was added to the LikedSongs playlist
                        Messenger.Default.Send(new ShowNotificationMessage(NotificationType.Added, TargetType.LikedSongs));
                    }
                }
            });
    }

    private bool HasASongLoaded()
    {
        return _SharedDataContext.CurrentPlayingSong != null;
    }

    

    private void SkipSong(Song songToPlay)
    {
        if (songToPlay != null)
            if (File.Exists(songToPlay.PcLink))
            {
                if (songToPlay.Id != CurrentPlayingSong?.Id)
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
