﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Muse.DB.Model;

[Table("SongDetail")]
public class SongDetail
{
    public int SongDetailId { get; set; }


    public int SongBasicId { get; set; }
    [ForeignKey("SongBasicId")]
    public SongBasic SongBasic { get; set; }

    public string? Lyrics { get; set; }

    public string? LocalUrl { get; set; }

    public string? BilibiliUrl { get; set; }

    public string? YoutubeUrl { get; set; }

    public string? OtherUrl { get; set; }
}