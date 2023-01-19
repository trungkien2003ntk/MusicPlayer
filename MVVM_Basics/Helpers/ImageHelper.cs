using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MVVM_Basics.Helpers
{

    public static class ImageHelper
    {
        private static Uri defaultSongCoverImageUri = new(@"pack://application:,,,/Images/DefaultSongCover.png");
        private static Uri defaultPlaylistCoverImageUri = new(@"pack://application:,,,/Images/DefaultPlaylistCoverImage.jpeg");

        public static BitmapImage LoadImage(byte[] imageData)
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

        public static BitmapImage GetDefaultSongCoverImage()
        {
            return new BitmapImage(defaultSongCoverImageUri);
        }

        public static BitmapImage GetDefaultPlaylistCoverImage()
        {
            return new BitmapImage(defaultPlaylistCoverImageUri);
        }
    }
}
