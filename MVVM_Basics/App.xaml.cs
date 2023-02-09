using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM_Basics.Factories;
using MVVM_Basics.Interfaces;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using MVVM_Basics.Views;
using System.Reflection;
using System;
using System.Windows;
using MVVM_Basics.Services;

namespace MVVM_Basics;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }


	public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<MusicPlayerVpContext>();
                services.AddSingleton<ISharedDataContext, SharedDataContext>();
                services.AddTransient<IPlaylistManager, PlaylistManager>();

                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<SideBarViewModel>();
                services.AddTransient<HomePageViewModel>();
                services.AddTransient<SearchPageViewModel>();
                services.AddTransient<LibraryPageViewModel>();
                services.AddSingleton<PlaylistPageViewModel>();
                services.AddTransient<LikedSongsPageViewModel>();
                services.AddTransient<SongControlViewModel>();
                services.AddTransient<TitleBarViewModel>();
                services.AddTransient<QueuePageViewModel>();

                services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });

            }).Build();


        // Make the menus appear from left to right
        LeftAlignContextMenu();
    }


    private static void LeftAlignContextMenu()
    {
        var menuDropAlignmentField = typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
        Action setAlignmentValue = () =>
        {
            if (SystemParameters.MenuDropAlignment && menuDropAlignmentField != null) menuDropAlignmentField.SetValue(null, false);
        };
        setAlignmentValue();
        SystemParameters.StaticPropertyChanged += (sender, e) => { setAlignmentValue(); };
    }


    protected override async void OnStartup(StartupEventArgs e)
    {
		await AppHost!.StartAsync();

		var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
		startupWindow.Show();

        base.OnStartup(e);
    }


    protected override async void OnExit(ExitEventArgs e)
    {
		await AppHost!.StopAsync();

        base.OnExit(e);
    }
}
