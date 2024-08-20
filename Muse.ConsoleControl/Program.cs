using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muse.DB.Configuration;

namespace Muse.ConsoleControl;

public class Program
{
    static void Main()
    {
        IServiceProvider serviceProvider = ConfigureServices();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            // Example database operation
            var songs = dbContext.SongBasic.ToList();
            foreach (var song in songs)
            {
                Console.WriteLine($"Song: {song.Title}");
            }
        }

    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
        services.AddDbContext<MyDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        return services.BuildServiceProvider();

    }
}