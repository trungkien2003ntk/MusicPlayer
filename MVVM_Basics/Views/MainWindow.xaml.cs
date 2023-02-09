using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Helpers;
using MVVM_Basics.Views.MessageAndNotification;
using System;
using System.Windows;

namespace MVVM_Basics.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Messenger.Default.Register<SongAlreadyInPlaylistMessage>(this, HandleSongAlreadyInPlaylistMessage);
        Messenger.Default.Register<ShowNotificationMessage>(this, HandleShowNotificationMessage);
    }

    private void HandleShowNotificationMessage(ShowNotificationMessage message)
    {
        toastControl.Content = new CustomizeToast(message.NotificationType, message.TargetType);
    }

    private void HandleSongAlreadyInPlaylistMessage(SongAlreadyInPlaylistMessage message)
    {
        var msgBox = new CustomMessageBox(message.PlaylistName);

        msgBox.ShowDialog();

        message.Callback(msgBox.DialogResult);
    }
}
