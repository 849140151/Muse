using System.Security.Cryptography.X509Certificates;
using HtmlAgilityPack;

namespace Muse.AudioProcessor.LyricCrawler;

public interface IWebCrawler
{

    Task GetLyricsAsync(string songName);


    void GetContentList();

    void CheckCreateFolder();

    void GetHtmlDoc();




}