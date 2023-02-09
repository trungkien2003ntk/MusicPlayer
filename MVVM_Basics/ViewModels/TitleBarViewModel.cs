using MVVM_Basics.EventAndCommandHandlers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class TitleBarViewModel : ViewModelBase
{
    public ICommand CloseWindowCommand { get; set; }
    public ICommand MaximizeWindowCommand { get; set; }
    public ICommand MinimizeWindowCommand { get; set; }

    public TitleBarViewModel()
    {
        CloseWindowCommand = new RelayCommand<Window>
        (
            (p) => { return true; },
            (p) => 
            {
                Window parent = p;

                if (parent!=null)
                    parent.Close();
            }
        );

        MaximizeWindowCommand = new RelayCommand<Window>
        (
            (p) => { return true; },
            (p) =>
            {
                Window parent = p;

                if (parent != null)
                {
                    if (parent.WindowState != WindowState.Maximized)
                    {
                        parent.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        parent.WindowState = WindowState.Normal;
                    }
                }
            }
        );

        MinimizeWindowCommand = new RelayCommand<Window>
        (
            (p) => { return true; },
            (p) =>
            {
                Window parent = p;

                if (parent != null)
                    parent.WindowState = WindowState.Minimized;
            }
        );
    }

    //FrameworkElement GetRootParent(UserControl p)
    //{
    //    FrameworkElement parent = p;

    //    while (parent.Parent != null)
    //    {
    //        parent = (parent.Parent as FrameworkElement)!;
    //    }

    //    return parent;
    //}
}
