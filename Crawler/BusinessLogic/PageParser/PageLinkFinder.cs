using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.PageParser
{
    public class PageLinkFinder
    {
        public static IEnumerable<string> FindPageUrls(string pageContent)
        {
            List<string> urls = new List<string>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            IEnumerable<HtmlNode> aNodes = htmlDocument.DocumentNode.Descendants("a");

            foreach (HtmlNode aNode in aNodes)
            {
                HtmlAttribute atr = aNode.Attributes.FirstOrDefault(a => a.Name == "href");
                if (atr != null)
                {
                    string url = atr.Value;
                    url = Regex.Replace(url, "^(http|https)://", "");

                    if (url != null) urls.Add(url);
                }
            }

            return urls;
        }
    }
}
