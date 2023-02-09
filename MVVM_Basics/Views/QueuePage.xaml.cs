using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVM_Basics.Views;

public partial class QueuePage : UserControl
{
    public QueuePage()
    {
        InitializeComponent();

        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);

        DataContext = App.AppHost!.Services.GetRequiredService<QueuePageViewModel>();
    }

    private void ListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
        if (r.VisualHit.GetType() != typeof(ListViewItem))
            lvNextUp.UnselectAll();
    }
}
