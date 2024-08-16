namespace Muse.UI.Model;

public class Song
{
    public long SongId { get; set; }
    public string Path { get; set; }
    public string Performers { get; set; }
    public string Title { get; set; }
    public string Duration { get; set; }
    public string? Album { get; set; }
    public string? BilibiliUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? OtherUrl { get; set; }
}