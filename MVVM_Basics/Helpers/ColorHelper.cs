using System.Windows.Media;

namespace MVVM_Basics.Helpers;

public static class ColorHelper
{
    public static SolidColorBrush GetGradientColorAtIndex(LinearGradientBrush brush, int index)
    {
        return new SolidColorBrush(brush.GradientStops[index].Color);
    }
}
