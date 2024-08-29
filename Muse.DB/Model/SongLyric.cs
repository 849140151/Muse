using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Muse.DB.Model;

[Table("SongLyrics")]
public class SongLyric
{

    public int SongLyricId { get; set; }

    public int SongBasicId { get; set; }
    [ForeignKey("SongBasicId")]
    public SongBasic? SongBasic { get; set; }

    public TimeSpan LyricTimeStamp { get; set; }

    public string? Romaji { get; set; }

    public string? Kanji { get; set; }
    
    public string? English { get; set; }

    public string? Chinese { get; set; }


}