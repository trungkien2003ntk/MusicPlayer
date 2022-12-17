using Id3;
using Microsoft.Identity.Client;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;

namespace MVVM_Basics.ViewModels
{
    public class CurrentPlayingSongViewModel : BaseViewModel
    {
        public static Song CurrentPlayingSong = new Song();
        public static bool HasSongLoaded = false;

        private string _MusicPath = "C:\\Users\\ADMIN\\Downloads\\Music\\Songs\\Tăng Duy Tân - Bên Trên Tầng Lầu - Official Lyric Video.mp3";
        public string MusicPath { get => _MusicPath; set => _MusicPath = value; }

        private BitmapImage _Thumbnail;
        public BitmapImage Thumbnail { get => _Thumbnail; set { _Thumbnail = value; OnPropertyChanged(); } }

        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }
        
        private string _Artist;
        public string Artist { get => _Artist; set { _Artist = value; OnPropertyChanged(); } }



        public CurrentPlayingSongViewModel()
        {
            // Set for testing UI
            CurrentPlayingSong.PcLink = MusicPath;
            HasSongLoaded = true;

            using (var mp3 = new Mp3(MusicPath))
            {
                Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);

                byte[] bytes = Encoding.Default.GetBytes("Kiến");
                Title = tag.Title.ToString();
                Artist = tag.Artists.ToString();

                Thumbnail = LoadImage(tag.Pictures[0].PictureData);
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public static void ReadID3()
        {
            string[] musicFiles = Directory.GetFiles(@"C:\Users\ADMIN\Downloads\Music\Songs", "*.mp3");

            foreach (string musicFile in musicFiles)
            {
                using (var mp3 = new Mp3(musicFile))
                {
                    Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);

                    byte[] bytes = Encoding.Default.GetBytes("Kiến");
                    string title = Encoding.UTF32.GetString(bytes);

                    Console.WriteLine("Title: {0}", title);
                    Console.WriteLine("Artist: {0}", tag.Artists.ToString());
                    Console.WriteLine("Album: {0}", tag.Album.ToString());
                    Console.WriteLine("Album: {0}", tag.Pictures[0].MimeType);
                    Console.ReadKey(true);
                }
            }
        }
    }
}
