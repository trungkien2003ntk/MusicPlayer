using MVVM_Basics.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM_Basics.Views;


public partial class LikedSongsPage : UserControl
{
    public LikedSongsPage()
    {
        InitializeComponent();
        
        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);
    }

    private void lvAllSongs_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;

            var parent = scrollViewerMain as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }

    private void scrollViewerMain_MouseEnter(object sender, MouseEventArgs e)
    {
        scrollViewerMain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    }

    private void scrollViewerMain_MouseLeave(object sender, MouseEventArgs e)
    {
        scrollViewerMain.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
    }
}
