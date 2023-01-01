using Id3;
using MVVM_Basics.Interfaces;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace MVVM_Basics.ViewModels
{
    public class SongControlViewModel : ViewModelBase, ISoundPlayer
    {
        #region Static 
        public static Queue<Song> songQueue = new Queue<Song>();
        public static Song CurrentPlayingSong = new Song();
        public static bool HasSongLoaded = false;
        #endregion Static 


        #region Fields
        private string _MusicPath = "C:\\Users\\ADMIN\\Downloads\\Music\\Songs\\Tăng Duy Tân - Bên Trên Tầng Lầu - Official Lyric Video.mp3";
        private string _Title;
        private string _Artist;
        private BitmapImage _Thumbnail;

        private Uri _Source;
        private bool _IsPlaying;
        #endregion Fields


        #region Properties
        public string MusicPath { get => _MusicPath; set => _MusicPath = value; }
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }
        public string Artist { get => _Artist; set { _Artist = value; OnPropertyChanged(); } }
        public BitmapImage Thumbnail { get => _Thumbnail; set { _Thumbnail = value; OnPropertyChanged(); } }

        public Uri Source { get => _Source; set { _Source = value; OnPropertyChanged(); } }
        public bool IsPlaying
        {
            get => _IsPlaying;
            set
            {
                _IsPlaying = value;
                if (HasSongLoaded)
                    if (_IsPlaying)
                        Play();
                    else
                        Pause();
            }
        }
        #endregion Properties


        #region Event
        public event Action Play;
        public event Action Pause;
        #endregion Event


        #region Constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SongControlViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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

            _IsPlaying = false;

            if (HasSongLoaded)
            {
                _Source = new Uri(CurrentPlayingSong.PcLink);
            }
        }
        #endregion Constructor


        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return new BitmapImage();
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

        // For reference
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
