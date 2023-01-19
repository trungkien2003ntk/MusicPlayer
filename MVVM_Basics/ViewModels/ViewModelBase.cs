using MVVM_Basics.EventAndCommandHandlers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public enum PageType
{
    HomePage,
    SearchPage,
    PlaylistPage,
    LibraryPage,
    QueuePage,
}

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
