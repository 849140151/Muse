using System.Text;

namespace Muse.AudioProcessor.LyricProcessor;
using NMeCab.Specialized;

public static class LyricTokenizer
{
    public static string ParseLyric(string lyric)
    {
        StringBuilder splitText = new StringBuilder();
        using (var tagger = MeCabIpaDicTagger.Create())
        {
            var nodes = tagger.Parse(lyric);
            foreach (var node in nodes)
            {
                splitText.Append(node.Surface);
                splitText.Append('/');
            }
        }

        string splitLyric = splitText.Remove(splitText.Length-1, 1).ToString();
        return splitLyric;
    }


    
}