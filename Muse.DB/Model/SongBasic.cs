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

    [Column(TypeName = "nvarchar(20)")]
    public string? Hiragana { get; set; }

    [Column(TypeName = "nvarchar(5)")]
    public string Duration { get; set; }

    [Column(TypeName = "nvarchar(10)")]
    public string? Album { get; set; }

    public SongDetail? SongDetail { get; set; }

}