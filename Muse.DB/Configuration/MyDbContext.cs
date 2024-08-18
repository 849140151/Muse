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
        string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        // // only assert the one who use configuration
        // modelBuilder.ApplyConfiguration(new SongBasicConfig());
    }

}