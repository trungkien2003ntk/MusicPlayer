using System.Windows;
using System.Windows.Controls;

namespace MVVM_Basics.ResourcesXAML
{
    public partial class PlaylistResource : System.Windows.ResourceDictionary
    {
        public PlaylistResource()
        {
            
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            var thisContextMenu = sender as ContextMenu;
            ListView listviewParent = thisContextMenu.PlacementTarget as ListView;

            listviewParent.SelectedIndex = -1;
        }
    }
}
