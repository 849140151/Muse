namespace Muse.AudioProcessor.SoundTrackOperator;

public class CommonFunc
{
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