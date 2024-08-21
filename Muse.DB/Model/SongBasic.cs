using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muse.DB.Model;
[Table("SongBasic")]
public class SongBasic
{

    public int SongBasicId { get; set; }


    [Column(TypeName = "nvarchar(10)")]
    public string Performers { get; set; }

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


    #region rewrite Equals method and GetHashCode method for using dinstinct function in LINQ
    // rewrite Equals method
    public override bool Equals(object? obj)
    {
        if (obj is SongBasic other)
        {
            return Title == other.Title &&
                   Performers == other.Performers &&
                   Duration == other.Duration;
        }
        return false;
    }

    // rewrite GetHashCode method
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + (Title?.GetHashCode() ?? 0);
            hash = hash * 31 + (Performers?.GetHashCode() ?? 0);
            hash = hash * 31 + Duration.GetHashCode();
            return hash;
        }
    }

    #endregion

}