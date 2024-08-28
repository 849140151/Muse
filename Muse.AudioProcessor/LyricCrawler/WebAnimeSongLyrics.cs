using System.Text;
using HtmlAgilityPack;
using Muse.AudioProcessor.Utils;

namespace Muse.AudioProcessor.LyricCrawler;

public class WebAnimeSongLyrics
{
    //Todo: need to exrtact the Full Version

    private readonly string _webUrl;
    private HtmlDocument? _htmlDocument;


    /// <summary>
    ///     Giving a songUrl while trying to construct
    /// </summary>
    /// <param name="webUrl"></param>
    public WebAnimeSongLyrics(string webUrl)
    {
        _webUrl = webUrl;
    }

    public string[] NodeArray = { "kanjilyrics", "romajilyrics", "englishlyrics"};

    public async Task GetRawLyricsAsync()
    {
        _htmlDocument = await CrawlerUtils.GetHtmlDocumentAsync(_webUrl);
    }

    /// <summary>
    ///     A flag for extract full version lyric
    /// </summary>
    public bool IsOnlyFullVersion { get; set; }



    /// <summary>
    ///     Modify the raw html text to selfnode, so that the lyric can be searched by selfnode Tag
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public HtmlDocument ModifyHtml(string html)
    {
        StringBuilder sbHtml = new StringBuilder(html);
        sbHtml.Replace("<br>", "</selfnode><br><selfnode>");
        sbHtml.Replace("</div>", "</selfnode><br></div>");
        string newHtml = sbHtml.ToString();
        var htmlDocument = new HtmlDocument();
        // Console.WriteLine(newHtml);
        htmlDocument.LoadHtml(newHtml);
        return htmlDocument;
    }


    public List<string>? KanjiList { get; set; } = [];
    public List<string>? RomajiList { get; set; } = [];
    public List<string>? EnglishList { get; set; } = [];

    /// <summary>
    ///     Get all 3 different lyrics and store it in to properties
    /// </summary>
    public void GetLyrics()
    {
        foreach (string node in NodeArray)
        {
            if (_htmlDocument != null)
            {
                string? rawNode = _htmlDocument.DocumentNode.SelectSingleNode($"//div[@class='{node}']").InnerHtml;
                HtmlDocument modifyHtml = ModifyHtml(rawNode);
                HtmlNodeCollection selfNodes = modifyHtml.DocumentNode.SelectNodes("//selfnode");
                switch (node)
                {
                    case "kanjilyrics":
                        foreach (HtmlNode selfNode in selfNodes) KanjiList.Add(selfNode.InnerText);
                        break;
                    case "romajilyrics":
                        foreach (HtmlNode selfNode in selfNodes) RomajiList.Add(selfNode.InnerText);
                        break;
                    case "englishlyrics":
                        foreach (HtmlNode selfNode in selfNodes) EnglishList.Add(selfNode.InnerText);
                        break;
                }

            }
        }
    }


}