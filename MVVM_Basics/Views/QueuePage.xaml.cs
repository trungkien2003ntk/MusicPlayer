using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.ViewModels;
using System.Windows.Controls;

namespace MVVM_Basics.Views;

public partial class QueuePage : UserControl
{
    public QueuePage()
    {
        InitializeComponent();

        DataContext = App.AppHost!.Services.GetRequiredService<QueuePageViewModel>();
    }
}
