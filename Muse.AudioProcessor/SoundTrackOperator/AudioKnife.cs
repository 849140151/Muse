using Muse.DB.Model;
using TagLib;
using File = TagLib.File;

namespace Muse.AudioProcessor.SoundTrackOperator;

public static class AudioKnife
{
    private static File? _audio;
    public static IPicture? Cover { get; set; }

    public static void Load(string songPath)
    {
        _audio?.Dispose();
        try
        {
            _audio = File.Create(songPath);
            Cover = _audio.Tag.Pictures?[0];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process audio file: {Path.GetFileName(songPath)}");
            Console.WriteLine($"Error message: {ex.Message}");
        }
    }

    public static void ReadAudioPerformers()
    {
        var performers = _audio.Tag.Performers;
        Console.WriteLine(performers.Count());
        foreach (var performer in performers) Console.WriteLine(performer);
    }

    // public static IPicture[]? GetAudioCover()
    // {
    //     var pictures = _audio.Tag.Pictures;
    //     return pictures;
    // }


    /// <summary>
    ///     Read audio tags from a file
    /// </summary>
    /// <param name="songPath"></param>
    /// <returns></returns>
    public static SongBasic? ReadAudioTags(string songPath)
    {
        try
        {
            var audio = File.Create(songPath);
            // Console.WriteLine(audio.Properties.Duration);
            var Song = new SongBasic
            {
                Title = audio.Tag.Title,
                Performers = audio.Tag.Performers is { Length: > 0 }
                    ? string.Join(", ", audio.Tag.Performers)
                    : "Unknown",
                Album = audio.Tag.Album,
                Duration = audio.Properties.Duration,
                SongDetail = new SongDetail
                {
                    LocalUrl = songPath
                }
            };
            return Song;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process audio file: {Path.GetFileName(songPath)}");
            Console.WriteLine($"Error message: {ex.Message}");
        }

        return null;
    }


    /// <summary>
    ///     Filter audio files in a folder
    /// </summary>
    /// <remarks>
    ///     Only support mp3, wav, flac, aac, m4a
    /// </remarks>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static List<string> FilterAudioFiles(string folderPath)
    {
        string[] audioExtensions = [".mp3", ".wav", ".flac", ".aac", ".m4a"];
        var audioFiles = Directory.EnumerateFiles(folderPath)
            .Where(file => audioExtensions.Contains(Path.GetExtension(file).ToLower()))
            .ToList();
        return audioFiles;
    }
}