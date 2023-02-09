using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVM_Basics.Views; 

public partial class HomePage : UserControl 
{ 
    public HomePage()
    { 
        InitializeComponent();

        DataContext = App.AppHost!.Services.GetRequiredService<HomePageViewModel>();

        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);
    }

    private void Border_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Border thisBorder = (sender as Border)!;

        if (e.ClickCount == 2)
        {
            Messenger.Default.Send(new ChangeSongMessage((Song)thisBorder.Tag));
        }
    }
}
