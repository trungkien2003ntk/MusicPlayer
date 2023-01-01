﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM_Basics.Factories;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using MVVM_Basics.Views;
using System.Windows;

namespace MVVM_Basics;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddTransient<MainWindowViewModel>();
				services.AddTransient<SideBarViewModel>();
				services.AddTransient<HomePageViewModel>();
				services.AddTransient<SearchPageViewModel>();

				services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });
			}).Build();
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
