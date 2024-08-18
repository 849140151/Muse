using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muse.DB.Model;
[Table("SongBasic")]
public class SongBasic
{

    public int SongBasicId { get; set; }


    [Column(TypeName = "nvarchar(10)")]
    public string Performers { get; set; }
    // Todo: Performers in TaglibSharp is a string[], need to find out if there is string[1] existed

    [Column(TypeName = "nvarchar(20)")]
    public string Title { get; set; }

    // Duration only used for saving the data
    public TimeSpan Duration { get; set; }
    // FormattedDuration is used for displaying, Binding by SongListView
    [NotMapped]
    public string FormattedDuration => $"{Duration.Minutes:D2}:{Duration.Seconds:D2}";
    // Kill the hours, don't think it will have hours in songs

    [Column(TypeName = "nvarchar(10)")]
    public string? Album { get; set; }

    public SongDetail? SongDetail { get; set; }

}