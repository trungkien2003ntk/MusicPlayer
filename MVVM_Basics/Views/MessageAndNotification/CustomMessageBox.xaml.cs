using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVVM_Basics.Views.MessageAndNotification;

public enum MessageBoxType
{
    // First MessageBox
    AlreadyAddedToPlaylist
}

public partial class CustomMessageBox : Window, INotifyPropertyChanged
{
    private string _PlaylistName;


    private string _MessageBoxMessage;
    public string MessageBoxMessage
    {
        get { return _MessageBoxMessage; }
        set { _MessageBoxMessage = value; OnPropertyChanged(); }
    }


    private string _MessageBoxTitle;
    public string MessageBoxTitle
    {
        get { return _MessageBoxTitle; }
        set { _MessageBoxTitle = value; OnPropertyChanged(); }
    }


    private string _YesBtnContent;
    public string YesBtnContent
    {
        get { return _YesBtnContent; }
        set { _YesBtnContent = value; OnPropertyChanged(); }
    }


    private string _NoBtnContent;
    public string NoBtnContent
    {
        get { return _NoBtnContent; }
        set { _NoBtnContent = value; OnPropertyChanged(); }
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CustomMessageBox(MessageBoxType messageBoxType)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        InitializeComponent();

        // Set the owner for the startup location
        Owner = Application.Current.MainWindow;

        // Set DataContext for binding to the string in this class
        DataContext = this;

        InitializeText(messageBoxType);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CustomMessageBox(string playlistName, MessageBoxType messageBoxType = MessageBoxType.AlreadyAddedToPlaylist)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        InitializeComponent();

        // Set the owner for the startup location
        Owner = Application.Current.MainWindow;

        // Set DataContext for binding to the string in this class
        DataContext = this;

        _PlaylistName = playlistName;

        InitializeText(messageBoxType);
    }

    protected override void OnDeactivated(EventArgs e)
    {
        base.OnDeactivated(e);

        if (DialogResult == null)
        {
            Close();
        }
    }

    private void InitializeText(MessageBoxType messageBoxType)
    {
        if (messageBoxType == MessageBoxType.AlreadyAddedToPlaylist)
        {
            MessageBoxMessage = $"This is already in your \"{_PlaylistName}\" playlist";
            MessageBoxTitle = "Already added";
            YesBtnContent = "Add anyway";
            NoBtnContent = "Don't add";
        }
    }

    private void btnYes_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void btnNo_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }


    // Implement InotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
