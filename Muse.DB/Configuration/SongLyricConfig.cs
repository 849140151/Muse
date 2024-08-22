using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muse.DB.Model;

namespace Muse.DB.Configuration;

public class SongLyricConfig : IEntityTypeConfiguration<SongLyric>
{
    public void Configure(EntityTypeBuilder<SongLyric> builder)
    {
        var timeSpanConverter = new ValueConverter<TimeSpan, double>(
            v => v.TotalSeconds, // TimeSpan convert to double
            v => TimeSpan.FromSeconds(v) // Seconds convert to TimeSpan
        );

        builder.Property(e => e.LyricTimeStamp)
            .HasConversion(timeSpanConverter);

        builder.HasIndex(e => e.SongBasicId);


    }
}