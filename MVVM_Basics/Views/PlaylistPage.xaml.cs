using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.ViewModels;
using System;
using System.Windows.Controls;

namespace MVVM_Basics.Views;

public partial class PlaylistPage : UserControl
{
    public PlaylistPage()
    {
        InitializeComponent();

        DataContext = App.AppHost!.Services.GetRequiredService<PlaylistPageViewModel>();
    }
}
