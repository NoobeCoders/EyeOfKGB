using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.PageParser
{
    public class PageLinkFinder
    {
        public IEnumerable<string> FindPageUrls(string pageContent)
        {
            List<string> urls = new List<string>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            IEnumerable<HtmlNode> aNodes = htmlDocument.DocumentNode.Descendants("a");

            foreach (HtmlNode aNode in aNodes)
            {
                string url = aNode.Attributes.FirstOrDefault(a => a.Name == "href").Value;

                if (url != null) urls.Add(url);
            }

            return urls;
        }
    }
}
