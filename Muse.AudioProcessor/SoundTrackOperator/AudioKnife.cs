using Muse.DB.Model;
using TagLib;

namespace Muse.AudioProcessor.SoundTrackOperator;

public static class AudioKnife
{
    private static TagLib.File? _audio;

    public static void Load(string songPath)
    {
        _audio?.Dispose();
        try
        {
            _audio = TagLib.File.Create(songPath);
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
        foreach (var performer in performers)
        {
            Console.WriteLine(performer);
        }
    }

    public static IPicture[]? ReadAudioPic()
    {
        var pictures = _audio.Tag.Pictures;
        return pictures;
    }


    /// <summary>
    /// Read audio tags from a file
    /// </summary>
    /// <param name="songPath"></param>
    /// <returns></returns>
    public static SongBasic? ReadAudioTags(string songPath)
    {
        try
        {
            var audio = TagLib.File.Create(songPath);
            // Console.WriteLine(audio.Properties.Duration);
            SongBasic Song = new SongBasic
            {
                Title = audio.Tag.Title,
                Performers = audio.Tag.Performers is { Length: > 0 } ? string.Join(", ", audio.Tag.Performers) : "Unknown",
                Album = audio.Tag.Album,
                Duration = audio.Properties.Duration,
                SongDetail = new SongDetail
                {
                    LocalUrl = songPath,
                    Lyrics = audio.Tag.Lyrics
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
    /// Filter audio files in a folder
    /// </summary>
    /// <remarks>
    /// Only support mp3, wav, flac, aac, m4a
    /// </remarks>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static List<string> FilterAudioFiles(string folderPath)
    {
        string[] audioExtensions = [".mp3", ".wav", ".flac", ".aac", ".m4a"];
        List<string> audioFiles = Directory.EnumerateFiles(folderPath)
            .Where(file => audioExtensions.Contains(Path.GetExtension(file).ToLower()))
            .ToList();
        return audioFiles;
    }

}