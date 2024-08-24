using System.Text.RegularExpressions;
using Muse.DB.Configuration;
using Muse.DB.Model;

namespace Muse.AudioProcessor.SoundTrackOperator;

public class MusicencLyricsOperator
{
    private readonly MyDbContext _dbContext;
    private string[]? LyricLines {get; set;}
    private int songId {get; set;}


    public MusicencLyricsOperator(MyDbContext dbContext, string lyricPath, string songTitle)
    {
        _dbContext = dbContext;
        LyricLines = File.ReadAllLines(lyricPath);
        songId = GetSongBasicId(songTitle);

    }


    public void SaveSongLyrics()
    {
        if (LyricLines == null)
        {
            Console.WriteLine("Error in SaveSongLyrics(), the LyricLines is null");
            return;
        }
        foreach (string lyric in LyricLines)
        {
            MatchCollection allMatches = Regex.Matches(lyric, @"(?<timestamp>\[\d{2}:\d{2}\])(?<lyric>.*)");
            foreach (Match match in allMatches)
            {
                string timestampString = match.Groups["timestamp"].Value;
                TimeSpan timestamp = StringToTimeSpan(timestampString);

                var existedSongLyric = _dbContext.SongLyric
                    .FirstOrDefault(s => s.SongBasicId == songId && s.LyricTimeStamp == timestamp);
                if (existedSongLyric is null)
                {
                    var newLyric = new SongLyric()
                    {
                        LyricTimeStamp = timestamp,
                        Japanese = match.Groups["lyric"].Value,
                        SongBasicId = songId
                    };
                    _dbContext.SongLyric.Add(newLyric);
                    _dbContext.SaveChanges();
                }
                else
                {
                    existedSongLyric.Chinese = match.Groups["lyric"].Value;
                    _dbContext.SaveChanges();
                }
            }
        }
    }

    /// <summary>
    /// Intermediate version, only for testing (the timestamp is modified to sting for this version)
    /// </summary>
    // public void SaveSongLyrics()
    // {
    //     foreach (string lyric in LyricLines)
    //     {
    //         MatchCollection allMatches = Regex.Matches(lyric, @"(?<timestamp>\[\d{2}:\d{2}\])(?<lyric>.*)");
    //         foreach (Match match in allMatches)
    //         {
    //             string timestamp = match.Groups["timestamp"].Value;
    //
    //             var existedSongLyric = _dbContext.SongLyric
    //                 .FirstOrDefault(s => s.SongBasicId == songId && s.LyricTimeStamp == timestamp);
    //             if (existedSongLyric is null)
    //             {
    //                 var newLyric = new SongLyric()
    //                 {
    //                     LyricTimeStamp = timestamp,
    //                     Japanese = match.Groups["lyric"].Value,
    //                     SongBasicId = songId
    //                 };
    //                 _dbContext.SongLyric.Add(newLyric);
    //                 _dbContext.SaveChanges();
    //
    //             }
    //             else
    //             {
    //                 existedSongLyric.Chinese = match.Groups["lyric"].Value;
    //                 _dbContext.SaveChanges();
    //             }
    //         }
    //     }
    // }

    public int GetSongBasicId(string songTitle)
    {
        int songId  = _dbContext.SongBasic
            .Where(s => s.Title == songTitle)
            .Select(s => s.SongBasicId)
            .FirstOrDefault();
        return songId;
    }

    public TimeSpan StringToTimeSpan(string timeStamp)
    {
        string trimmedTimeStamp = timeStamp.Trim('[', ']');
        TimeSpan timeSpan = TimeSpan.Parse(trimmedTimeStamp);
        return timeSpan;
    }


}