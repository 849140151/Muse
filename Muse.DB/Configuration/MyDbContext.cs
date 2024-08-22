using Microsoft.EntityFrameworkCore;
using Muse.DB.Model;

namespace Muse.DB.Configuration;

public class MyDbContext : DbContext
{
    public DbSet<SongBasic> SongBasic { get; set; }

    public DbSet<SongDetail> SongDetail { get; set; }

    public DbSet<SongLyric> SongLyric { get; set; }

    // For using DI
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    // After the ctor above, need a whole new function for Database Migration
    public MyDbContext() : base()
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder);
            string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
            string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
            optionsBuilder.UseSqlite($"Data Source={dbPath};");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        // // only assert the one who use configuration
        // modelBuilder.ApplyConfiguration(new SongBasicConfig());
    }

}