using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muse.DB.Model;

namespace Muse.DB.Configuration;

public class SongBasicConfig : IEntityTypeConfiguration<SongBasic>
{
    public void Configure(EntityTypeBuilder<SongBasic> builder)
    {

        // Config the converter for the Duration property
        // Saving the timespan as seconds in the sqlite
        var timeSpanConverter = new ValueConverter<TimeSpan, double>(
            v => v.TotalSeconds, // TimeSpan convert to double
            v => TimeSpan.FromSeconds(v) // Seconds convert to TimeSpan
        );

        builder.Property(e => e.Duration)
            .HasConversion(timeSpanConverter);
    }
}