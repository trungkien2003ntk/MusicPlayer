using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using TagLib;

namespace MVVM_Basics.Helpers;

public static class Mp3Helper
{
    public static string GetTitle(string songPcPath)
    {
        var file = TagLib.File.Create(songPcPath);

        string title = file.Tag.Title;

        if (!string.IsNullOrEmpty(title)) 
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(file.Tag.Title.ToLower());

        return Path.GetFileNameWithoutExtension(songPcPath);
    }

    public static BitmapImage GetCoverImage(string songPcPath)
    {
        var file = TagLib.File.Create(songPcPath);

        if (file.Tag.Pictures.Length != 0)
            return ImageHelper.LoadImage(file.Tag.Pictures[0].Data.Data);

        return ImageHelper.GetDefaultPlaylistCoverImage();
    }

    public static double GetDuration(string songPcPath)
    {
        var file = TagLib.File.Create(songPcPath);

        return file.Properties.Duration.TotalSeconds;
    }

    public static string[] GetArtistNames(string songPcPath)
    {
        var file = TagLib.File.Create(songPcPath);

        // Get the Not-separated artist names. Ex: Adams, Kids
        string artistNamesNotSeparated = file.Tag.FirstPerformer;
        
        if (!string.IsNullOrEmpty(artistNamesNotSeparated))
        {
            // Separate the artist name in string and store in an array
            string[] artistNames = artistNamesNotSeparated.Split(',');

            // Validate artist names
            for (int i = 0; i < artistNames.Length; i++)
            {
                artistNames[i] = new CultureInfo("en-US", false).TextInfo.ToTitleCase(artistNames[i].ToLower());
                artistNames[i] = artistNames[i].TrimStart();
            }

            return artistNames;
        }

        return new string[]{ "V.A."};
    }
}
