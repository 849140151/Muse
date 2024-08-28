using System.Text;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muse.AudioProcessor.LyricCrawler;
using Muse.AudioProcessor.SoundTrackOperator;
using Muse.AudioProcessor.Utils;
using Muse.DB.Configuration;
using NAudio.Wave;
using TagLib;
using File = System.IO.File;


namespace Muse.ConsoleControl;

public class Program
{

    public static async Task Main(string[] args)
    {
        string webUrl = "https://www.animesonglyrics.com/sousou-no-frieren/haru";
        var sl = new WebAnimeSongLyrics(webUrl);
        await sl.GetRawLyricsAsync();
        sl.GetLyrics();
        foreach (string node in sl.NodeArray)
        {
            switch (node)
            {
                case "kanjilyrics":
                    foreach (string kanji in sl.KanjiList) Console.WriteLine(kanji);
                    break;
                case "romajilyrics":
                    foreach (string romaji in sl.RomajiList) Console.WriteLine(romaji);
                    break;
                case "englishlyrics":
                    foreach (string english in sl.EnglishList) Console.WriteLine(english);
                    break;
            }
        }
    }

    ///using database
    // public static void Main()
    // {
    //     string lyricPath = @"D:\0.ForAllProject\Mnemosyne\Mnemosyne.Lyrics\Lyrics\musicenc.com\ワスレガタキ.lrc";
    //     string songTitle = "ワスレガタキ";
    //     IServiceProvider serviceProvider = ConfigureServices();
    //
    //     using (var scope = serviceProvider.CreateScope())
    //     {
    //         var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    //         var lyricsOperator = new MusicencLyricsOperator(dbContext, lyricPath, songTitle);
    //         lyricsOperator.SaveSongLyrics();
    //     }
    // }
    //
    // private static IServiceProvider ConfigureServices()
    // {
    //     var services = new ServiceCollection();
    //     string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
    //     string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
    //     services.AddDbContext<MyDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
    //
    //     return services.BuildServiceProvider();
    //
    // }




}