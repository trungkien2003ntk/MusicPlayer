using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.ViewModels;
using System.Windows.Controls;

namespace MVVM_Basics.Views;

public partial class TitleBar : UserControl
{
    public TitleBar()
    {
        InitializeComponent();
        DataContext = App.AppHost!.Services.GetRequiredService<TitleBarViewModel>();
    }
}
