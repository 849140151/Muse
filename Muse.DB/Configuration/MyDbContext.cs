using Microsoft.EntityFrameworkCore;
using Muse.DB.Model;

namespace Muse.DB.Configuration;

public class MyDbContext : DbContext
{
    public DbSet<SongBasic> SongBasic { get; set; }

    public DbSet<SongDetail> SongDetail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=Muse.sqlite;");
    }

}