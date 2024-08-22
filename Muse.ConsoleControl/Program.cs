using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muse.AudioProcessor.SoundTrackOperator;
using Muse.DB.Configuration;
using NAudio.Wave;


namespace Muse.ConsoleControl;

public class Program
{
    // static void Main()
    // {
    //     AudioKnife.Load(@"D:\Project\Muse\Muse.Resources\Music\Songs\ヨルシカ - 晴る.mp3");
    //     var pic = AudioKnife.ReadAudioPic();
    //     foreach (var p in pic)
    //     {
    //         Console.WriteLine(p.ToString());
    //     }
    // }

    /// <summary>
    /// Audio player test
    /// </summary>
    static void Main()
    {
        AudioPlayer.Load(@"D:\Project\Muse\Muse.Resources\Music\Songs\ヨルシカ - 晴る.mp3");

        Console.WriteLine("Playing audio");
        AudioPlayer.Play();
        AudioPlayer.StartTimer();
        while (AudioPlayer.waveOut.PlaybackState == PlaybackState.Playing)
        {
            // AudioPlayer.ShowCurrentTime();
            Thread.Sleep(100);
        }
        // Thread.Sleep(5000); // 播放5秒
        //
        // Console.WriteLine("lowering volume of audio");
        // AudioPlayer.SetVolume(0.5f); // 设置音量为50%
        //
        // Console.WriteLine("Pausing audio");
        // AudioPlayer.Pause();
        // System.Threading.Thread.Sleep(2000); // 暂停2秒
        // //
        // Console.WriteLine("Resuming audio");
        // AudioPlayer.Play();
        // Thread.Sleep(3000); // 播放3秒
        //
        // Console.WriteLine("Jumping to 10 seconds");
        // AudioPlayer.SetPosition(TimeSpan.FromSeconds(10));
        // AudioPlayer.Play();
        // System.Threading.Thread.Sleep(5000); // 播放5秒
        //
        // Console.WriteLine("Stopping audio");
        // AudioPlayer.Stop();
        //
        AudioPlayer.Dispose();
    }


    /// <summary>
    /// DataBase test
    /// </summary>
    /// <returns></returns>
    // static void Main()
    // {
    //     IServiceProvider serviceProvider = ConfigureServices();
    //
    //     using (var scope = serviceProvider.CreateScope())
    //     {
    //         var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    //
    //         // Example database operation
    //         var songs = dbContext.SongBasic.ToList();
    //         foreach (var song in songs)
    //         {
    //             Console.WriteLine($"Song: {song.Title}");
    //         }
    //     }
    //
    // }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
        services.AddDbContext<MyDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        return services.BuildServiceProvider();

    }
}