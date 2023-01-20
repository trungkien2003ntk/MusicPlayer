using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Basics.ViewModels;

public enum PageType
{
    HomePage,
    SearchPage,
    PlaylistPage,
    LibraryPage,
    QueuePage,
    LikedSongsPage,
}

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Cleanup() { }
}
