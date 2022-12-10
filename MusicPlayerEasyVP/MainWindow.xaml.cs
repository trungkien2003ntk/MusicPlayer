using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayerEasyVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> lst;
        public MainWindow()
        {
            InitializeComponent();
            lst = new List<string>
                {
                    "Playlist #1",
                    "Playlist #2",
                    "Playlist #3",
                    "Playlist #4",
                    "Playlist #5",
                    "Playlist #6",
                    "Playlist #7",
                    "Playlist #8",
                    "Playlist #9",
                    "Playlist #10",};
            playlistList.ItemsSource = lst;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
