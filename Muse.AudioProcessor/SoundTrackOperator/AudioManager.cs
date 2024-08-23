using Muse.DB.Model;
using TagLib;
using File = TagLib.File;

namespace Muse.AudioProcessor.SoundTrackOperator;

public class AudioManager
{
    private File? _audio;
    public IPicture? Cover { get; set; }
    public SongBasic? SongBasic { get; set; }

    public void Load(string songPath)
    {
        _audio?.Dispose();
        try
        {
            _audio = File.Create(songPath);
            Cover = _audio.Tag.Pictures?[0];
            SongBasic = GetSongBasic(songPath) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process audio file: {Path.GetFileName(songPath)}");
            Console.WriteLine($"Error message: {ex.Message}");
        }
    }

    /// <summary>
    ///     Read audio tags from a file
    /// </summary>
    /// <param name="songPath"></param>
    /// <returns></returns>
    private SongBasic? GetSongBasic(string songPath)
    {
        if (_audio is null) return null;
        var song = new SongBasic
        {
            Title = _audio.Tag.Title,
            Performers = _audio.Tag.Performers is { Length: > 0 }
                ? string.Join(", ", _audio.Tag.Performers)
                : "Unknown",
            Album = _audio.Tag.Album,
            Duration = _audio.Properties.Duration,
            SongDetail = new SongDetail { LocalUrl = songPath }
        };
        return song;
    }
}