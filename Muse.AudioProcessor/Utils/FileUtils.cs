namespace Muse.AudioProcessor.Utils;

public static class FileUtils
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

    /// <summary>
    /// Check or create lyric folder for storing lyrics for different lyrics website
    /// </summary>
    /// <param name="webName"></param>
    /// <returns></returns>
    public static string GetLyricFolder(string webName)
    {
        string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        string lyricFolder = Path.Combine(projectFolder, "Lyrics");

        // Different folder for different lyrics website
        string lyricFromWeb = Path.Combine(lyricFolder, webName);

        if (!Directory.Exists(lyricFromWeb))
        {
            Directory.CreateDirectory(lyricFromWeb);
        }
        return lyricFromWeb;
    }

    /// <summary>
    /// Write content to file
    /// </summary>
    /// <param name="path">File's path</param>
    /// <param name="content">The content of the file</param>
    public static void WriteToFile(string path, string content)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}