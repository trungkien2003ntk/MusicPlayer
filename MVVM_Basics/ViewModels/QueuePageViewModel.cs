using Microsoft.Extensions.DependencyInjection;
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
        
    }
}
