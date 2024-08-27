using HtmlAgilityPack;

namespace Muse.AudioProcessor.Utils;

public class CrawlerUtils
{




    /// <summary>
    /// Get html document from url
    /// </summary>
    /// <remarks>
    /// This method only take one url, need to be change
    /// </remarks>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<HtmlDocument?> GetHtmlDocumentAsync(string url)
    {
        try
        {
            // Send a GET request to the specified URL
            using (var httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                return htmlDocument;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP Request error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General error: {ex.Message}");
        }
        return null;
    }





}