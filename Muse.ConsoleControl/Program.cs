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

    // public static void Main(string[] args)
    // {
    //     string filePath = @"C:\Users\84914\OneDrive\Desktop\htmlDoc.txt";
    //     byte[] fileBytes = File.ReadAllBytes(filePath);
    //     string rawLyric = Encoding.UTF8.GetString(fileBytes);
    //     var htmlDocument = new HtmlDocument();
    //     htmlDocument.LoadHtml(rawLyric);
    //     string? rawKanji = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='kanjilyrics']").InnerHtml;
    //     string? rawRomaji = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='romajilyrics']").InnerHtml;
    //     string? rawEnglish = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='englishlyrics']").InnerHtml;
    //
    //     // Console.WriteLine(rawKanji);
    //
    //     HtmlDocument newHtmlDocument = CrawlerUtils.ModifyHtml(rawKanji);
    //     HtmlNodeCollection selfNodes = newHtmlDocument.DocumentNode.SelectNodes("//selfnode");
    //     foreach (HtmlNode selfNode in selfNodes) Console.WriteLine(selfNode.InnerText);
    //     Console.WriteLine($"------------------------------{selfNodes.Count}--------------------------------------------");
    //
    //     HtmlDocument newHtmlDocument2 = CrawlerUtils.ModifyHtml(rawRomaji);
    //     HtmlNodeCollection selfNodes2 = newHtmlDocument2.DocumentNode.SelectNodes("//selfnode");
    //     foreach (HtmlNode selfNode in selfNodes2) Console.WriteLine(selfNode.InnerText);
    //     Console.WriteLine($"------------------------------{selfNodes2.Count}--------------------------------------------");
    //
    //
    //     HtmlDocument newHtmlDocument3 = CrawlerUtils.ModifyHtml(rawEnglish);
    //     HtmlNodeCollection selfNodes3 = newHtmlDocument3.DocumentNode.SelectNodes("//selfnode");
    //     foreach (HtmlNode selfNode in selfNodes3) Console.WriteLine(selfNode.InnerText);
    //     Console.WriteLine($"------------------------------{selfNodes3.Count}--------------------------------------------");
    //
    // }

    // public static void Main(string[] args)
    // {
    //     string filePath = @"C:\Users\84914\OneDrive\Desktop\htmlDoc.txt";
    //     byte[] fileBytes = File.ReadAllBytes(filePath);
    //     string rawLyric = Encoding.UTF8.GetString(fileBytes);
    //     var htmlDocument = new HtmlDocument();
    //     htmlDocument.LoadHtml(rawLyric);
    //     HtmlNode contentNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='kanjilyrics']");
    //     // string allLyrics = contentNode.InnerText;
    //     // Console.WriteLine(allLyrics);
    //
    //     string kanjiLyrics = contentNode.InnerHtml;
    //     HtmlDocument newHtmlDocument = CrawlerUtils.ModifyHtml(kanjiLyrics);
    //     HtmlNodeCollection selfNodes = newHtmlDocument.DocumentNode.SelectNodes("//selfnode");
    //     foreach (HtmlNode selfNode in selfNodes)
    //     {
    //         Console.WriteLine(selfNode.InnerText);
    //     }
    //
    // }

    // public static async Task Main(string[] args)
    // {
    //     string webUrl = @"https://www.animesonglyrics.com/dr-stone-new-world/wasuregataki";
    //     var wa = new WebAnimeSongLyrics(webUrl);
    //     await wa.GetLyricsAsync("waseli");
    // }


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

    /// <summary>
    /// Audio player test
    /// </summary>
    // static void Main()
    // {
    //     AudioPlayer.Load(@"D:\Project\Muse\Muse.Resources\Music\Songs\ヨルシカ - 晴る.mp3");
    //
    //     Console.WriteLine("Playing audio");
    //     AudioPlayer.Play();
    //     AudioPlayer.StartTimer();
    //     while (AudioPlayer.waveOut.PlaybackState == PlaybackState.Playing)
    //     {
    //         // AudioPlayer.ShowCurrentTime();
    //         Thread.Sleep(100);
    //     }
    //     Thread.Sleep(5000); // 播放5秒
    //
    //     Console.WriteLine("lowering volume of audio");
    //     AudioPlayer.SetVolume(0.5f); // 设置音量为50%
    //
    //     Console.WriteLine("Pausing audio");
    //     AudioPlayer.Pause();
    //     System.Threading.Thread.Sleep(2000); // 暂停2秒
    //     //
    //     Console.WriteLine("Resuming audio");
    //     AudioPlayer.Play();
    //     Thread.Sleep(3000); // 播放3秒
    //
    //     Console.WriteLine("Jumping to 10 seconds");
    //     AudioPlayer.SetPosition(TimeSpan.FromSeconds(10));
    //     AudioPlayer.Play();
    //     System.Threading.Thread.Sleep(5000); // 播放5秒
    //
    //     Console.WriteLine("Stopping audio");
    //     AudioPlayer.Stop();
    //
    //     AudioPlayer.Dispose();
    // }


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


}